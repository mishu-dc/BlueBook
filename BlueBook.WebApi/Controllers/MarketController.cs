using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Configurations;
using BlueBook.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlueBook.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MarketController : ApiController
    {
        private readonly UnitOfWork _unitOfWork = null;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MarketController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger.Info("Market Web Api Controller Initialized successfully");
        }

        [Route("api/markets/")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMarketHierarchiesByCodeAndNameAsync()
        {
            try
            {
                List<MarketHierarchy> markets = null;
                markets = await _unitOfWork.MarketHierarchies.FindAsync(x=>x.Id>0);

                _logger.Info(string.Format("Total {0} markets(s) found", markets.Count()));

                var records = markets.Where(x => x.ParentId == null)
                    .Select(m => new MarketHierarchyDto()
                    {
                        Id = m.Id,
                        Code = m.Code,
                        Name = m.Code + "-" + m.Name,
                        Type = m.Type,
                        ParentId = m.ParentId != null ? m.ParentId.Value : -1,
                        Chields = GetChildren(markets, m.Id)
                    }).ToList();

                return Ok(records);
            }
            catch (Exception ex)
            {
                if (_logger.IsDebugEnabled)
                {
                    _logger.Debug(ex);
                    return InternalServerError(ex);
                }
                return InternalServerError();
            }
        }

        private List<MarketHierarchyDto> GetChildren(List<MarketHierarchy> mhs, int? parentId)
        {
            var records = mhs.Where(x => x.ParentId == parentId.Value)
                    .Select(m => new MarketHierarchyDto()
                    {
                        Id = m.Id,
                        Code = m.Code,
                        Name = m.Code + "-" + m.Name,
                        Type = m.Type,
                        ParentId = m.ParentId != null ? m.ParentId.Value : -1,
                        Chields = GetChildren(mhs, m.Id)
                    }).ToList();

            return records;
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMarketHierarchyAsync(int id)
        {
            try
            {

                MarketHierarchy market = await _unitOfWork.MarketHierarchies.GetAsync(id);

                if (market == null)
                {
                    _logger.Info(string.Format("No market found with id", id));
                    return NotFound();
                }

                return Ok(new MarketHierarchyDto()
                {
                    Id = market.Id,
                    Code = market.Code,
                    Name = market.Name,
                    Type = market.Type,
                    ParentId = market.ParentId
                });
            }
            catch (Exception ex)
            {
                if (_logger.IsDebugEnabled)
                {
                    _logger.Debug(ex);
                    return InternalServerError(ex);
                }
                return InternalServerError();
            }
        }

        [HttpPost]
        [ActionName("Save")]
        [Route("api/markets/save")]
        public async Task<IHttpActionResult> SaveMarketHierarchyAsync(MarketHierarchyDto record)
        {
            MarketHierarchy market = null;
            MarketHierarchy parent = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (record == null)
                    {
                        return BadRequest();
                    }

                    if (record.ParentId != null)
                    {
                        parent = _unitOfWork.MarketHierarchies.Get(record.ParentId.Value);
                        if(parent == null)
                        {
                            return BadRequest("Invalid parent market id");
                        }
                    }

                    if (record.Id != null)
                    {
                        market = _unitOfWork.MarketHierarchies.Get(record.Id.Value);
                        if (market == null)
                        {
                            return BadRequest("Invalid market id");
                        }

                        market.UpdatedBy = "web:api";
                        market.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        market = new MarketHierarchy();
                        _unitOfWork.MarketHierarchies.Add(market);

                        market.CreatedBy = "web:api";
                    }

                    market.Code = record.Code;
                    market.Name = record.Name;
                    market.Type = record.Type;
                    market.Parent = parent;
                    

                    await _unitOfWork.CompleteAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (_logger.IsDebugEnabled)
                    {
                        _logger.Debug(ex);
                        return InternalServerError(ex);
                    }
                    return InternalServerError();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

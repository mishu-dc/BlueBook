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
    public class DistributorController : ApiController
    {
        private readonly UnitOfWork _unitOfWork = null;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public DistributorController(UnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
            _logger.Info("Distributor Web Api Controller Initialized successfully");
        }

        [Route("api/distributors/{code?}/{name?}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDistributorsByCodeAndNameAsync(string code="", string name="")
        {
            try
            {
                IEnumerable<Distributor> distributors = null;
                distributors = await _unitOfWork.Distributors.GetDistributorsAsync(code, name);

                _logger.Info(string.Format("Total {0} distributor(s) found", distributors.Count()));

                return Ok(distributors.Select(d => new DistributorDto()
                {
                    Id = d.Id,
                    Code = d.Code,
                    Name = d.Name,
                    Address = d.Address
                }));
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

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDistributorAsync(int id)
        {
            try
            {
                
                Distributor distributor = await _unitOfWork.Distributors.GetAsync(id);

                if (distributor == null)
                {
                    _logger.Info(string.Format("No distributor found with id", id));
                    return NotFound();
                }
                
                return Ok(new DistributorDto()
                {
                    Id = distributor.Id,
                    Code = distributor.Code,
                    Name = distributor.Name,
                    Address = distributor.Address
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
        [Route("api/distributors/save")]
        public async Task<IHttpActionResult> SaveDistributorAsync(DistributorDto record)
        {
            Distributor distributor = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (record == null)
                    {
                        return BadRequest();
                    }

                    if (record.Id != null)
                    {
                        distributor = _unitOfWork.Distributors.Get(record.Id.Value);
                        if (distributor == null)
                        {
                            return NotFound();
                        }

                        distributor.UpdatedBy = "web:api";
                        distributor.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        distributor = new Distributor();
                        _unitOfWork.Distributors.Add(distributor);

                        distributor.CreatedBy = "web:api";
                    }

                    distributor.Code = record.Code;
                    distributor.Name = record.Name;
                    distributor.Address = record.Address;

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

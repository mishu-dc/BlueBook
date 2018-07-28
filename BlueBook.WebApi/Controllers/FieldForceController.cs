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
    public class FieldForceController : ApiController
    {
        private readonly UnitOfWork _unitOfWork = null;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FieldForceController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger.Info("Field Force Web Api Controller Initialized successfully");
        }

        [Route("api/fieldforces/{code?}/{name?}/{fieldforceId?}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFieldForcesByCodeAndNameAsync(string code = "", string name = "", string sortBy = "name", string direction = "asc", int page = 1, int size = 10, int distributorId = -1)
        {
            try
            {
                IEnumerable<FieldForce> fieldforces = null;
                fieldforces = await _unitOfWork.FieldForces.GetFieldForceByPageAsync(page, size, sortBy, direction, code, name, distributorId);
                int total = await _unitOfWork.FieldForces.GetTotalFieldForceAsync(page, size, sortBy, direction, code, name, distributorId);

                _logger.Info(string.Format("Total {0} brnad(s) found", fieldforces.Count()));

                var items = fieldforces.Select(ff => new FieldForceDto()
                {
                    Id = ff.Id,
                    Code = ff.Code,
                    Name = ff.Name,
                    AddressLine1 = ff.Address != null ? ff.Address.AddressLine1 : string.Empty,
                    AddressLine2 = ff.Address != null ? ff.Address.AddressLine2 : string.Empty,
                    City = ff.Address != null ? ff.Address.City : string.Empty,
                    State = ff.Address != null ? ff.Address.State : string.Empty,
                    Zip = ff.Address != null ? ff.Address.Zip : string.Empty,
                    Email = ff.Email,
                    Phone = ff.Phone,
                    MarketHierarchyId = ff.MarketHierarchyId,
                    MarketHierarchy = ff.MarketHierarchy != null ? new MarketHierarchyDto() { Code = ff.MarketHierarchy.Code, Name = ff.MarketHierarchy.Name, Id = ff.MarketHierarchy.Id, ParentId = ff.MarketHierarchy.ParentId, Type = ff.MarketHierarchy.Type } : null,
                    Distributors = ff.Distributors != null ? ff.Distributors.Select(d => new DistributorDto() { Id = d.Id, Code = d.Code, Address = d.Address, Name = d.Name }).ToList() : new List<DistributorDto>(),
                    DistributorIds = ff.Distributors!=null? ff.Distributors.Select(d=>d.Id).ToList(): new List<int>()
                });

                return Ok(new { total, items });
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
        public async Task<IHttpActionResult> GetFieldForceAsync(int id)
        {
            try
            {

                FieldForce ff = await _unitOfWork.FieldForces.GetAsync(id);

                if (ff == null)
                {
                    _logger.Info(string.Format("No fieldforce found with id", id));
                    return NotFound();
                }

                return Ok(new FieldForceDto()
                {
                    Id = ff.Id,
                    Code = ff.Code,
                    Name = ff.Name,
                    AddressLine1 = ff.Address != null ? ff.Address.AddressLine1 : string.Empty,
                    AddressLine2 = ff.Address != null ? ff.Address.AddressLine2 : string.Empty,
                    City = ff.Address != null ? ff.Address.City : string.Empty,
                    State = ff.Address != null ? ff.Address.State : string.Empty,
                    Zip = ff.Address != null ? ff.Address.Zip : string.Empty,
                    Email = ff.Email,
                    Phone = ff.Phone,
                    MarketHierarchyId = ff.MarketHierarchyId,
                    MarketHierarchy = ff.MarketHierarchy != null ? new MarketHierarchyDto() { Code = ff.MarketHierarchy.Code, Name = ff.MarketHierarchy.Name, Id = ff.MarketHierarchy.Id, ParentId = ff.MarketHierarchy.ParentId, Type = ff.MarketHierarchy.Type } : null,
                    Distributors = ff.Distributors != null ? ff.Distributors.Select(d => new DistributorDto() { Id = d.Id, Code = d.Code, Address = d.Address, Name = d.Name }).ToList() : new List<DistributorDto>(),
                    DistributorIds = ff.Distributors != null ? ff.Distributors.Select(d => d.Id).ToList() : new List<int>()
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
        [Route("api/fieldforces/save")]
        [ActionName("Save")]
        public async Task<IHttpActionResult> SaveFieldForceAsync(FieldForceDto record)
        {
            FieldForce fieldforce = null;
            MarketHierarchy market = null;
            FieldForceAddress address = null;
            List<Distributor> distributors = new List<Distributor>();

            if (ModelState.IsValid)
            {
                try
                {
                    if (record == null)
                    {
                        return BadRequest();
                    }

                    if (record.MarketHierarchyId.HasValue)
                    {
                        market = await _unitOfWork.MarketHierarchies.GetAsync(record.MarketHierarchyId.Value);
                        if (market == null)
                        {
                            return BadRequest("Invalid market id");
                        }
                    }

                    foreach (int id in record.DistributorIds)
                    {
                        Distributor distributor = _unitOfWork.Distributors.Get(id);
                        if (distributor == null)
                        {
                            return BadRequest("Invalid fistributor id");
                        }
                        distributors.Add(distributor);
                    }


                    if (record.Id != null)
                    {
                        fieldforce = _unitOfWork.FieldForces.Get(record.Id.Value);
                        if (fieldforce == null)
                        {
                            return NotFound();
                        }

                        fieldforce.UpdatedBy = "web:api";
                        fieldforce.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        fieldforce = new FieldForce();
                        _unitOfWork.FieldForces.Add(fieldforce);

                        fieldforce.CreatedBy = "web:api";
                    }

                    fieldforce.Distributors.Clear();
                    fieldforce.Distributors = distributors;

                    fieldforce.MarketHierarchy = market;
                    
                    fieldforce.Code = record.Code;
                    fieldforce.Name = record.Name;
                    fieldforce.Email = record.Email;
                    fieldforce.Phone = record.Phone;

                    address = new FieldForceAddress();
                    address.AddressLine1 = record.AddressLine1;
                    address.AddressLine2 = record.AddressLine2;
                    address.City = record.City;
                    address.State = record.State;
                    address.Zip = record.Zip;

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

﻿using BlueBook.DataAccess.Entities;
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
    public class BrandsController : ApiController
    {
        private readonly UnitOfWork _unitOfWork = null;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BrandsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger.Info("Brand Web Api Controller Initialized successfully");
        }

        [Route("api/brands/{code?}/{name?}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetBrandsByCodeAndNameAsync(string code="", string name="")
        {
            try
            {
                IEnumerable<Brand> brands = null;
                brands = await _unitOfWork.Brands.GetBrandsAsync(code, name);

                _logger.Info(string.Format("Total {0} brnad(s) found", brands.Count()));

                return Ok(brands.Select(d => new BrandDto()
                {
                    id = d.Id,
                    code = d.Code,
                    name = d.Name
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
        public async Task<IHttpActionResult> GetBrandAsync(int id)
        {
            try
            {
                
                Brand brand = await _unitOfWork.Brands.GetBrandAsync(id);

                if (brand == null)
                {
                    _logger.Info(string.Format("No brand found with id", id));
                    return NotFound();
                }
                
                return Ok(new BrandDto()
                {
                    id = brand.Id,
                    code = brand.Code,
                    name = brand.Name
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
        [Route("api/brands/save")]
        public async Task<IHttpActionResult> SaveBrandAsync(BrandDto record)
        {
            Brand brand = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (record == null)
                    {
                        return BadRequest();
                    }

                    if (record.id != null)
                    {
                        brand = _unitOfWork.Brands.Get(record.id.Value);
                        if (brand == null)
                        {
                            return NotFound();
                        }

                        brand.UpdatedBy = "web:api";
                        brand.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        brand = new Brand();
                        _unitOfWork.Brands.Add(brand);

                        brand.CreatedBy = "web:api";
                    }

                    brand.Code = record.code;
                    brand.Name = record.name;

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

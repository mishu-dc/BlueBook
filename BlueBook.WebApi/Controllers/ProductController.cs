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
    public class ProductController : ApiController
    {
        private readonly UnitOfWork _unitOfWork = null;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProductController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger.Info("Product Web Api Controller Initialized successfully");
        }

        [Route("api/products/{code?}/{name?}/{productId?}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductsByCodeAndNameAsync(string code = "", string name = "", int brandId=-1)
        {
            try
            {
                IEnumerable<Product> products = null;
                products = await _unitOfWork.Products.GetProductsAsync(code, name, brandId);

                _logger.Info(string.Format("Total {0} brnad(s) found", products.Count()));

                var results = products.Select(d => new ProductDto()
                {
                    id = d.Id,
                    code = d.Code,
                    name = d.Name,
                    brandId = d.Brand != null ? d.Brand.Id : -1,
                    brandName = d.Brand != null ? d.Brand.Name : "-",
                    price = d.Price
                });

                return Ok(results);
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
        public async Task<IHttpActionResult> GetProductAsync(int id)
        {
            try
            {

                Product product = await _unitOfWork.Products.GetProductAsync(id);

                if (product == null)
                {
                    _logger.Info(string.Format("No product found with id", id));
                    return NotFound();
                }

                return Ok(new ProductDto()
                {
                    id = product.Id,
                    code = product.Code,
                    name = product.Name,
                    brandId = product.Brand!=null? product.Brand.Id: -1,
                    brandName = product.Brand!=null? product.Brand.Name: "-",
                    price = product.Price
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
        [Route("api/products/save")]
        [ActionName("Save")]
        public async Task<IHttpActionResult> SaveProductAsync(ProductDto record)
        {
            Product product = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (record == null)
                    {
                        return BadRequest();
                    }

                    Brand brand = _unitOfWork.Brands.Get(record.brandId);
                    if (brand == null)
                    {
                        return NotFound();
                    }

                    if (record.id != null)
                    {
                        product = _unitOfWork.Products.Get(record.id.Value);
                        if (product == null)
                        {
                            return NotFound();
                        }

                        product.UpdatedBy = "web:api";
                        product.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        product = new Product();
                        _unitOfWork.Products.Add(product);

                        product.CreatedBy = "web:api";
                    }

                    product.Code = record.code;
                    product.Name = record.name;
                    product.Brand = brand;
                    product.Price = record.price;

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

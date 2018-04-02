using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueBook.DataAccess.Tests
{
    [TestClass]
    public class ProductRepositoryTest:BaseTest
    {
        public ProductRepositoryTest()
            :base()
        {

        }

        [TestMethod]
        public void InsertProduct()
        {
            Brand brand = new Brand() { Code = "B1", Name = "Microsoft", CreatedBy = "Unit Test" };
            Product product = new Product() { Code  = "P1", Name = "Office 360", Price = 120.99, CreatedBy = "Unit Test", Brand = brand };

            UnitOfWork.Products.Add(product);

            UnitOfWork.Complete();

            Product dbProduct = UnitOfWork.Products.Get(product.Id);

            Assert.IsNotNull(dbProduct);
            Assert.AreEqual(product.Code, dbProduct.Code);
            Assert.AreEqual(product.CreatedBy, dbProduct.CreatedBy);
            Assert.AreEqual(product.Name, dbProduct.Name);

            Assert.IsNotNull(dbProduct.Brand);
            Assert.AreEqual(dbProduct.Brand.Code, brand.Code);
            Assert.AreEqual(dbProduct.Brand.Name, brand.Name);
        }

        [TestMethod]
        public async Task InsertProductAsync()
        {
            Brand brand = new Brand() { Code = "B1", Name = "Microsoft", CreatedBy = "Unit Test" };
            Product product = new Product() { Code = "P1", Name = "Office 360", Price = 120.99, CreatedBy = "Unit Test", Brand = brand };

            UnitOfWork.Products.Add(product);

            await UnitOfWork.CompleteAsync();

            Product dbProduct = await UnitOfWork.Products.GetAsync(product.Id);

            Assert.IsNotNull(dbProduct);
            Assert.AreEqual(product.Code, dbProduct.Code);
            Assert.AreEqual(product.CreatedBy, dbProduct.CreatedBy);
            Assert.AreEqual(product.Name, dbProduct.Name);

            Assert.IsNotNull(dbProduct.Brand);
            Assert.AreEqual(dbProduct.Brand.Code, brand.Code);
            Assert.AreEqual(dbProduct.Brand.Name, brand.Name);
        }

        [TestMethod]
        public void DeleteProduct()
        {
            Brand brand = new Brand() { Code = "B1", Name = "Microsoft", CreatedBy = "Unit Test" };
            Product product = new Product() { Code = "P1", Name = "Office 360", Price = 120.99, CreatedBy = "Unit Test", Brand = brand };

            UnitOfWork.Products.Add(product);

            UnitOfWork.Complete();

            Product dbProduct = UnitOfWork.Products.Get(product.Id);

            Assert.IsNotNull(dbProduct);

            UnitOfWork.Products.Remove(dbProduct);

            UnitOfWork.Complete();

            dbProduct = UnitOfWork.Products.Get(product.Id);

            Assert.IsNull(dbProduct);
        }

        [TestMethod]
        public async Task DeleteProductAsync()
        {
            Brand brand = new Brand() { Code = "B1", Name = "Microsoft", CreatedBy = "Unit Test" };
            Product product = new Product() { Code = "P1", Name = "Office 360", Price = 120.99, CreatedBy = "Unit Test", Brand = brand };

            UnitOfWork.Products.Add(product);

            await UnitOfWork.CompleteAsync();

            Product dbProduct = await UnitOfWork.Products.GetAsync(product.Id);

            Assert.IsNotNull(dbProduct);

            UnitOfWork.Products.Remove(dbProduct);

            await UnitOfWork.CompleteAsync();

            dbProduct = await UnitOfWork.Products.GetAsync(product.Id);

            Assert.IsNull(dbProduct);
        }

    }
}

using BlueBook.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Tests
{
    [TestClass]
    public class BrandRepository: BaseTest
    {
        public BrandRepository()
            :base()
        {            
        }

        [TestMethod]
        public void InsertBrand()
        {
            Brand brand = new Brand() { Code = "B1", CreatedBy = "Unit Test", Name = "Microsoft" };
            UnitOfWork.Brands.Add(brand);

            UnitOfWork.Complete();

            Brand dbBrand = UnitOfWork.Brands.Get(brand.Id);

            Assert.IsNotNull(dbBrand);
            Assert.AreEqual(brand.Code, dbBrand.Code);
            Assert.AreEqual(brand.CreatedBy, dbBrand.CreatedBy);
            Assert.AreEqual(brand.Name, dbBrand.Name);
        }

        [TestMethod]
        public void DeleteBrand()
        {
            Brand brand = new Brand() { Code = "B1", CreatedBy = "Unit Test", Name = "Microsoft" };
            UnitOfWork.Brands.Add(brand);

            UnitOfWork.Complete();

            UnitOfWork.Brands.Remove(brand);

            UnitOfWork.Complete();

            Brand dbBrand = UnitOfWork.Brands.Get(brand.Id);
            
            Assert.IsNull(dbBrand);
        }

        [TestMethod]
        public void InsertBrandAndProduct()
        {
            Brand brand = new Brand() { Code = "B1", CreatedBy = "Unit Test", Name = "Microsoft" };
            UnitOfWork.Brands.Add(brand);

            Product product1 = new Product() { Code = "P1", Name = "Asp.Net MVC", CreatedBy = "Unit Test", Price = 99.99 , Brand = brand };
            Product product2 = new Product() { Code = "P2", Name = "Asp.Net Web Api", CreatedBy = "Unit Test", Price = 88.88, Brand = brand };

            brand.Products.Add(product1);
            brand.Products.Add(product2);

            UnitOfWork.Complete();

            Brand dbBrand = UnitOfWork.Brands.Get(brand.Id);
            Product dbProduct1 = UnitOfWork.Products.Get(product1.Id);
            Product dbProduct2 = UnitOfWork.Products.Get(product2.Id);

            Assert.IsNotNull(dbBrand);
            Assert.AreEqual(brand.Code, dbBrand.Code);
            Assert.AreEqual(brand.CreatedBy, dbBrand.CreatedBy);
            Assert.AreEqual(brand.Name, dbBrand.Name);
            Assert.AreEqual(brand.Products.Count, brand.Products.Count);

            Assert.IsNotNull(dbProduct1);
            Assert.AreEqual(dbProduct1.Code, product1.Code);
            Assert.AreEqual(dbProduct1.Name, product1.Name);
            Assert.AreEqual(dbProduct1.Price, product1.Price);

            Assert.IsNotNull(dbProduct2);
            Assert.AreEqual(dbProduct2.Code, product2.Code);
            Assert.AreEqual(dbProduct2.Name, product2.Name);
            Assert.AreEqual(dbProduct2.Price, product2.Price);
        }

        [TestMethod]
        public void InsertBrands()
        {
            Brand brand1 = new Brand() { Code = "B1", CreatedBy = "Unit Test", Name = "Microsoft" };
            Brand brand2 = new Brand() { Code = "B2", CreatedBy = "Unit Test", Name = "Apple" };

            List<Brand> brands = new List<Brand>();
            brands.Add(brand1);
            brands.Add(brand2);

            UnitOfWork.Brands.Add(brands);
            UnitOfWork.Complete();

            var dbBrands = UnitOfWork.Brands.Find(x=>x.Id>0);
            
            Assert.IsNotNull(dbBrands);
            Assert.AreEqual(brands.Count, dbBrands.Count());
        }

        [TestMethod]
        public async Task InsertBrandAndProductAsync()
        {
            Brand brand = new Brand() { Code = "B1", CreatedBy = "Unit Test", Name = "Microsoft" };
            UnitOfWork.Brands.Add(brand);

            Product product1 = new Product() { Code = "P1", Name = "Asp.Net MVC", CreatedBy = "Unit Test", Price = 99.99, Brand = brand };
            Product product2 = new Product() { Code = "P2", Name = "Asp.Net Web Api", CreatedBy = "Unit Test", Price = 88.88, Brand = brand };

            brand.Products.Add(product1);
            brand.Products.Add(product2);

            await UnitOfWork.CompleteAsync();

            Brand dbBrand = await UnitOfWork.Brands.GetAsync(brand.Id);
            Product dbProduct1 = await UnitOfWork.Products.GetAsync(product1.Id);
            Product dbProduct2 = await UnitOfWork.Products.GetAsync(product2.Id);

            Assert.IsNotNull(dbBrand);
            Assert.AreEqual(brand.Code, dbBrand.Code);
            Assert.AreEqual(brand.CreatedBy, dbBrand.CreatedBy);
            Assert.AreEqual(brand.Name, dbBrand.Name);
            Assert.AreEqual(brand.Products.Count, brand.Products.Count);

            Assert.IsNotNull(dbProduct1);
            Assert.AreEqual(dbProduct1.Code, product1.Code);
            Assert.AreEqual(dbProduct1.Name, product1.Name);
            Assert.AreEqual(dbProduct1.Price, product1.Price);

            Assert.IsNotNull(dbProduct2);
            Assert.AreEqual(dbProduct2.Code, product2.Code);
            Assert.AreEqual(dbProduct2.Name, product2.Name);
            Assert.AreEqual(dbProduct2.Price, product2.Price);
        }

        [TestMethod]
        public async Task InsertBrandAsync()
        {
            Brand brand = new Brand() { Code = "B1", CreatedBy = "Unit Test", Name = "Microsoft" };
            UnitOfWork.Brands.Add(brand);

            await UnitOfWork.CompleteAsync();

            Brand dbBrand = await UnitOfWork.Brands.GetAsync(brand.Id);

            Assert.IsNotNull(dbBrand);
            Assert.AreEqual(brand.Code, dbBrand.Code);
            Assert.AreEqual(brand.CreatedBy, dbBrand.CreatedBy);
            Assert.AreEqual(brand.Name, dbBrand.Name);
        }

        [TestMethod]
        public async Task DeleteBrandAsync()
        {
            Brand brand = new Brand() { Code = "B1", CreatedBy = "Unit Test", Name = "Microsoft" };
            UnitOfWork.Brands.Add(brand);

            await UnitOfWork.CompleteAsync();

            UnitOfWork.Brands.Remove(brand);

            await UnitOfWork.CompleteAsync();

            Brand dbBrand = await UnitOfWork.Brands.GetAsync(brand.Id);

            Assert.IsNull(dbBrand);
        }


    }
}

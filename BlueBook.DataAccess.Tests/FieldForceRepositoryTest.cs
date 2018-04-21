using System;
using BlueBook.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Tests
{
    [TestClass]
    public class FieldForceRepositoryTest : BaseTest
    {
        public FieldForceRepositoryTest()
            : base()
        {

        }

        [TestMethod]
        public void InsertFieldForce()
        {
            FieldForceAddress address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", AddressLine2 = "Apt 2", State = "VA", Zip = "22306", City = "Alexandria", CreatedBy = "Unit Test" };
            FieldForce fieldForce = new FieldForce() { Address = address, Code = "F1", Email = "test@unittest.com", Name = "Faisal AHmed", Phone = "(571)-409-8511", CreatedBy = "Unit Test" };

            UnitOfWork.FieldForces.Add(fieldForce);
            UnitOfWork.Complete();

            FieldForce dbFieldForce = UnitOfWork.FieldForces.Get(fieldForce.Id);

            Assert.IsNotNull(dbFieldForce);

            Assert.AreEqual(dbFieldForce.Name, fieldForce.Name);
            Assert.AreEqual(dbFieldForce.Code, fieldForce.Code);
            Assert.AreEqual(dbFieldForce.Email, fieldForce.Email);
            Assert.AreEqual(dbFieldForce.Phone, fieldForce.Phone);

            Assert.IsNotNull(dbFieldForce.Address);
        }

        [TestMethod]
        public void InsertFieldForceWithDistributorAndMarketHierarchy()
        {
            MarketHierarchy nation = new MarketHierarchy() { Code = "M1", CreatedBy = "Unit Test", Name = "US", Type = MarketHierarchyType.Nation };
            MarketHierarchy region = new MarketHierarchy() { Code = "M2", CreatedBy = "Unit Test", Name = "Virginia", Type = MarketHierarchyType.Region, Parent = nation };
            MarketHierarchy territory = new MarketHierarchy() { Code = "M3", CreatedBy = "Unit Test", Name = "Alexandria", Type = MarketHierarchyType.Territory, Parent = region };
            MarketHierarchy route = new MarketHierarchy() { Code = "M4", CreatedBy = "Unit Test", Name = "Richmond Hwy", Type = MarketHierarchyType.Route, Parent = territory };

            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };


            FieldForceAddress address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", AddressLine2 = "Apt 2", State = "VA", Zip = "22306", City = "Alexandria", CreatedBy = "Unit Test" };
            FieldForce fieldForce = new FieldForce() { Address = address, Code = "F1", Email = "test@unittest.com", Name = "Faisal AHmed", Phone = "(571)-409-8511", CreatedBy = "Unit Test" };

            fieldForce.MarketHierarchy = route;
            fieldForce.Distributors.Add(distributor);


            UnitOfWork.MarketHierarchies.Add(route);
            UnitOfWork.MarketHierarchies.Add(territory);
            UnitOfWork.MarketHierarchies.Add(region);
            UnitOfWork.MarketHierarchies.Add(nation);

            UnitOfWork.Distributors.Add(distributor);

            UnitOfWork.FieldForces.Add(fieldForce);

            UnitOfWork.Complete();

            FieldForce dbFieldForce = UnitOfWork.FieldForces.Get(fieldForce.Id);

            Assert.IsNotNull(dbFieldForce);

            Assert.AreEqual(dbFieldForce.Name, fieldForce.Name);
            Assert.AreEqual(dbFieldForce.Code, fieldForce.Code);
            Assert.AreEqual(dbFieldForce.Email, fieldForce.Email);
            Assert.AreEqual(dbFieldForce.Phone, fieldForce.Phone);

            Assert.IsNotNull(dbFieldForce.MarketHierarchy);
            Assert.AreEqual(dbFieldForce.MarketHierarchy.Id, route.Id);
            Assert.AreEqual(dbFieldForce.MarketHierarchy.Code, route.Code);
            Assert.AreEqual(dbFieldForce.MarketHierarchy.Name, route.Name);

            Assert.AreEqual(1, dbFieldForce.Distributors.Count);
            Assert.IsNotNull(dbFieldForce.Distributors[0]);
            Assert.AreEqual(dbFieldForce.Distributors[0].Id, distributor.Id);
            Assert.AreEqual(dbFieldForce.Distributors[0].Name, distributor.Name);
            Assert.AreEqual(dbFieldForce.Distributors[0].Name, distributor.Name);
        }

        [TestMethod]
        public async Task InsertFieldForceWithDistributorAndMarketHierarchyAsync()
        {
            MarketHierarchy nation = new MarketHierarchy() { Code = "M1", CreatedBy = "Unit Test", Name = "US", Type = MarketHierarchyType.Nation };
            MarketHierarchy region = new MarketHierarchy() { Code = "M2", CreatedBy = "Unit Test", Name = "Virginia", Type = MarketHierarchyType.Region, Parent = nation };
            MarketHierarchy territory = new MarketHierarchy() { Code = "M3", CreatedBy = "Unit Test", Name = "Alexandria", Type = MarketHierarchyType.Territory, Parent = region };
            MarketHierarchy route = new MarketHierarchy() { Code = "M4", CreatedBy = "Unit Test", Name = "Richmond Hwy", Type = MarketHierarchyType.Route, Parent = territory };

            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };


            FieldForceAddress address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", AddressLine2 = "Apt 2", State = "VA", Zip = "22306", City = "Alexandria", CreatedBy = "Unit Test" };
            FieldForce fieldForce = new FieldForce() { Address = address, Code = "F1", Email = "test@unittest.com", Name = "Faisal AHmed", Phone = "(571)-409-8511", CreatedBy = "Unit Test" };

            fieldForce.MarketHierarchy = route;
            fieldForce.Distributors.Add(distributor);


            UnitOfWork.MarketHierarchies.Add(route);
            UnitOfWork.MarketHierarchies.Add(territory);
            UnitOfWork.MarketHierarchies.Add(region);
            UnitOfWork.MarketHierarchies.Add(nation);

            UnitOfWork.Distributors.Add(distributor);

            UnitOfWork.FieldForces.Add(fieldForce);

            await UnitOfWork.CompleteAsync();

            FieldForce dbFieldForce = await UnitOfWork.FieldForces.GetAsync(fieldForce.Id);

            Assert.IsNotNull(dbFieldForce);

            Assert.AreEqual(dbFieldForce.Name, fieldForce.Name);
            Assert.AreEqual(dbFieldForce.Code, fieldForce.Code);
            Assert.AreEqual(dbFieldForce.Email, fieldForce.Email);
            Assert.AreEqual(dbFieldForce.Phone, fieldForce.Phone);

            Assert.IsNotNull(dbFieldForce.MarketHierarchy);
            Assert.AreEqual(dbFieldForce.MarketHierarchy.Id, route.Id);
            Assert.AreEqual(dbFieldForce.MarketHierarchy.Code, route.Code);
            Assert.AreEqual(dbFieldForce.MarketHierarchy.Name, route.Name);

            Assert.AreEqual(1, dbFieldForce.Distributors.Count);
            Assert.IsNotNull(dbFieldForce.Distributors[0]);
            Assert.AreEqual(dbFieldForce.Distributors[0].Id, distributor.Id);
            Assert.AreEqual(dbFieldForce.Distributors[0].Name, distributor.Name);
            Assert.AreEqual(dbFieldForce.Distributors[0].Name, distributor.Name);
        }

        [TestMethod]
        public async Task InsertFieldForceAsync()
        {
            FieldForceAddress address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", AddressLine2 = "Apt 2", State = "VA", Zip = "22306", City = "Alexandria", CreatedBy = "Unit Test" };
            FieldForce fieldForce = new FieldForce() { Address = address, Code = "F1", Email = "test@unittest.com", Name = "Faisal AHmed", Phone = "(571)-409-8511", CreatedBy = "Unit Test" };

            UnitOfWork.FieldForces.Add(fieldForce);
            await UnitOfWork.CompleteAsync();

            FieldForce dbFieldForce = await UnitOfWork.FieldForces.GetAsync(fieldForce.Id);

            Assert.IsNotNull(dbFieldForce);

            Assert.AreEqual(dbFieldForce.Name, fieldForce.Name);
            Assert.AreEqual(dbFieldForce.Code, fieldForce.Code);
            Assert.AreEqual(dbFieldForce.Email, fieldForce.Email);
            Assert.AreEqual(dbFieldForce.Phone, fieldForce.Phone);
        }


        [TestMethod]
        public void DeleteFieldForce()
        {
            FieldForceAddress address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", AddressLine2 = "Apt 2", State = "VA", Zip = "22306", City = "Alexandria", CreatedBy = "Unit Test" };
            FieldForce fieldForce = new FieldForce() { Address = address, Code = "F1", Email = "test@unittest.com", Name = "Faisal AHmed", Phone = "(571)-409-8511", CreatedBy = "Unit Test" };

            UnitOfWork.FieldForces.Add(fieldForce);
            UnitOfWork.Complete();

            FieldForce dbFieldForce = UnitOfWork.FieldForces.Get(fieldForce.Id);

            Assert.IsNotNull(dbFieldForce);

            UnitOfWork.FieldForces.Remove(dbFieldForce);

            UnitOfWork.Complete();

            dbFieldForce = UnitOfWork.FieldForces.Get(fieldForce.Id);

            Assert.IsNull(dbFieldForce);
        }

        [TestMethod]
        public async Task DeleteFieldForceAsync()
        {
            FieldForceAddress address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", AddressLine2 = "Apt 2", State = "VA", Zip = "22306", City = "Alexandria", CreatedBy = "Unit Test" };
            FieldForce fieldForce = new FieldForce() { Address = address, Code = "F1", Email = "test@unittest.com", Name = "Faisal AHmed", Phone = "(571)-409-8511", CreatedBy = "Unit Test" };

            UnitOfWork.FieldForces.Add(fieldForce);
            await UnitOfWork.CompleteAsync();

            FieldForce dbFieldForce = await UnitOfWork.FieldForces.GetAsync(fieldForce.Id);

            Assert.IsNotNull(dbFieldForce);

            UnitOfWork.FieldForces.Remove(dbFieldForce);

            await UnitOfWork.CompleteAsync();

            dbFieldForce = await UnitOfWork.FieldForces.GetAsync(fieldForce.Id);

            Assert.IsNull(dbFieldForce);
        }

        [TestMethod]
        public void DeleteFieldForceWithDistributorAndMarketHierarchy()
        {
            MarketHierarchy nation = new MarketHierarchy() { Code = "M1", CreatedBy = "Unit Test", Name = "US", Type = MarketHierarchyType.Nation };
            MarketHierarchy region = new MarketHierarchy() { Code = "M2", CreatedBy = "Unit Test", Name = "Virginia", Type = MarketHierarchyType.Region, Parent = nation };
            MarketHierarchy territory = new MarketHierarchy() { Code = "M3", CreatedBy = "Unit Test", Name = "Alexandria", Type = MarketHierarchyType.Territory, Parent = region };
            MarketHierarchy route = new MarketHierarchy() { Code = "M4", CreatedBy = "Unit Test", Name = "Richmond Hwy", Type = MarketHierarchyType.Route, Parent = territory };

            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };


            FieldForceAddress address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", AddressLine2 = "Apt 2", State = "VA", Zip = "22306", City = "Alexandria", CreatedBy = "Unit Test" };
            FieldForce fieldForce = new FieldForce() { Address = address, Code = "F1", Email = "test@unittest.com", Name = "Faisal AHmed", Phone = "(571)-409-8511", CreatedBy = "Unit Test" };

            fieldForce.MarketHierarchy = route;
            fieldForce.Distributors.Add(distributor);


            UnitOfWork.MarketHierarchies.Add(route);
            UnitOfWork.MarketHierarchies.Add(territory);
            UnitOfWork.MarketHierarchies.Add(region);
            UnitOfWork.MarketHierarchies.Add(nation);

            UnitOfWork.Distributors.Add(distributor);

            UnitOfWork.FieldForces.Add(fieldForce);

            UnitOfWork.Complete();

            FieldForce dbFieldForce = UnitOfWork.FieldForces.Get(fieldForce.Id);

            Assert.IsNotNull(dbFieldForce);

            UnitOfWork.FieldForces.Remove(dbFieldForce);

            UnitOfWork.Complete();

            MarketHierarchy dbRoute = UnitOfWork.MarketHierarchies.Get(route.Id);
            Assert.IsNotNull(dbRoute);

            Distributor dbDistributor = UnitOfWork.Distributors.Get(distributor.Id);
            Assert.IsNotNull(dbDistributor);
        }
    }
}

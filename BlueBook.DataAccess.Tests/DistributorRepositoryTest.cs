using System;
using BlueBook.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Tests
{
    [TestClass]
    public class DistributorRepositoryTest : BaseTest
    {
        public DistributorRepositoryTest()
            : base()
        {

        }

        [TestMethod]
        public void InsertDistributor()
        {
            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };
            UnitOfWork.Distributors.Add(distributor);
            UnitOfWork.Complete();

            Distributor dbDistributor = UnitOfWork.Distributors.Get(distributor.Id);
            Assert.IsNotNull(dbDistributor);

            Assert.AreEqual(distributor.Code, dbDistributor.Code);
            Assert.AreEqual(distributor.Name, dbDistributor.Name);
        }

        [TestMethod]
        public void DeleteDistributor()
        {
            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };
            UnitOfWork.Distributors.Add(distributor);
            UnitOfWork.Complete();

            Distributor dbDistributor = UnitOfWork.Distributors.Get(distributor.Id);
            Assert.IsNotNull(dbDistributor);

            UnitOfWork.Distributors.Remove(dbDistributor);
            UnitOfWork.Complete();

            dbDistributor = UnitOfWork.Distributors.Get(distributor.Id);
            Assert.IsNull(dbDistributor);
        }

        [TestMethod]
        public async Task InsertDistributorAsync()
        {
            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };
            UnitOfWork.Distributors.Add(distributor);
            await UnitOfWork.CompleteAsync();

            Distributor dbDistributor = await UnitOfWork.Distributors.GetAsync(distributor.Id);
            Assert.IsNotNull(dbDistributor);

            Assert.AreEqual(distributor.Code, dbDistributor.Code);
            Assert.AreEqual(distributor.Name, dbDistributor.Name);
        }

        [TestMethod]
        public async Task DeleteDistributorAsync()
        {
            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };
            UnitOfWork.Distributors.Add(distributor);
            await UnitOfWork.CompleteAsync();

            Distributor dbDistributor = await UnitOfWork.Distributors.GetAsync(distributor.Id);
            Assert.IsNotNull(dbDistributor);

            UnitOfWork.Distributors.Remove(dbDistributor);
            await UnitOfWork.CompleteAsync();

            dbDistributor = await UnitOfWork.Distributors.GetAsync(distributor.Id);

            Assert.IsNull(dbDistributor);
        }

        [TestMethod]
        public void InsertDistributorWithFieldForce()
        {
            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };
            FieldForce fieldForce = new FieldForce() { Code = "F1", CreatedBy = "Unit Test", Address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", CreatedBy = "Unit Test", City = "Alexandria", State = "VA", Zip = "22306", AddressLine2 = "Apt 2" }, Name = "Faisal Ahmed", Phone = "571-409-8588" };

            distributor.FieldForces.Add(fieldForce);

            UnitOfWork.Distributors.Add(distributor);

            UnitOfWork.Complete();

            Distributor dbDistributor = UnitOfWork.Distributors.Get(distributor.Id);
            Assert.IsNotNull(dbDistributor);

            Assert.AreEqual(distributor.Code, dbDistributor.Code);
            Assert.AreEqual(distributor.Name, dbDistributor.Name);
            Assert.IsTrue(dbDistributor.FieldForces.Count > 0);
            Assert.AreEqual(dbDistributor.FieldForces[0].Code, fieldForce.Code);
            Assert.AreEqual(dbDistributor.FieldForces[0].Name, fieldForce.Name);
        }

        [TestMethod]
        public async Task InsertDistributorWithFieldForceAsync()
        {
            Distributor distributor = new Distributor() { Address = "9420 Key West Avenue", CreatedBy = "Unit Test", Code = "D1", Name = "DrFirst" };
            FieldForce fieldForce = new FieldForce() { Code = "F1", CreatedBy = "Unit Test", Address = new FieldForceAddress() { AddressLine1 = "7101 Richmond Hwy", CreatedBy = "Unit Test", City = "Alexandria", State = "VA", Zip = "22306", AddressLine2 = "Apt 2" }, Name = "Faisal Ahmed", Phone = "571-409-8588" };

            distributor.FieldForces.Add(fieldForce);

            UnitOfWork.Distributors.Add(distributor);

            await UnitOfWork.CompleteAsync();

            Distributor dbDistributor = UnitOfWork.Distributors.Get(distributor.Id);
            Assert.IsNotNull(dbDistributor);

            Assert.AreEqual(distributor.Code, dbDistributor.Code);
            Assert.AreEqual(distributor.Name, dbDistributor.Name);
            Assert.IsTrue(dbDistributor.FieldForces.Count > 0);
            Assert.AreEqual(dbDistributor.FieldForces[0].Code, fieldForce.Code);
            Assert.AreEqual(dbDistributor.FieldForces[0].Name, fieldForce.Name);
        }
        
    }
}

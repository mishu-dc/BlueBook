using System;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueBook.DataAccess.Tests
{
    [TestClass]
    public class MarketHierarchyRepositoryTest:BaseTest
    {
        public MarketHierarchyRepositoryTest()
            :base()
        {
        }

        [TestMethod]
        public void InsertMarketHierarchy()
        {
            MarketHierarchy nation = new MarketHierarchy() { Code = "M1", CreatedBy = "Unit Test", Name = "US", Type = MarketHierarchyType.Nation };
            MarketHierarchy region = new MarketHierarchy() { Code = "M2", CreatedBy = "Unit Test", Name = "Virginia", Type = MarketHierarchyType.Region, Parent = nation };
            MarketHierarchy territory = new MarketHierarchy() { Code = "M3", CreatedBy = "Unit Test", Name = "Alexandria", Type = MarketHierarchyType.Territory, Parent = region };
            MarketHierarchy route = new MarketHierarchy() { Code = "M4", CreatedBy = "Unit Test", Name = "Richmond Hwy", Type = MarketHierarchyType.Route, Parent = territory };

            UnitOfWork.MarketHierarchies.Add(route);
            UnitOfWork.MarketHierarchies.Add(territory);
            UnitOfWork.MarketHierarchies.Add(region);
            UnitOfWork.MarketHierarchies.Add(nation);
            
            UnitOfWork.Complete();

            MarketHierarchy dbRoute = UnitOfWork.MarketHierarchies.Get(route.Id);

            Assert.IsNotNull(dbRoute);
            Assert.IsNotNull(dbRoute.Parent);

            MarketHierarchy dbTerritory = dbRoute.Parent;

            Assert.AreEqual(dbTerritory.Code, territory.Code);
            Assert.AreEqual(dbTerritory.Name, territory.Name);
            Assert.AreEqual(dbTerritory.Type, territory.Type);

            MarketHierarchy dbRegion = dbTerritory.Parent;

            Assert.AreEqual(dbRegion.Code, region.Code);
            Assert.AreEqual(dbRegion.Name, region.Name);
            Assert.AreEqual(dbRegion.Type, region.Type);

            MarketHierarchy dbNation = dbRegion.Parent;

            Assert.AreEqual(dbNation.Code, nation.Code);
            Assert.AreEqual(dbNation.Name, nation.Name);
            Assert.AreEqual(dbNation.Type, nation.Type);
        }

        [TestMethod]
        public void DeleteMarketHierarchy()
        {
            MarketHierarchy nation = new MarketHierarchy() { Code = "M1", CreatedBy = "Unit Test", Name = "US", Type = MarketHierarchyType.Nation };
            MarketHierarchy region = new MarketHierarchy() { Code = "M2", CreatedBy = "Unit Test", Name = "Virginia", Type = MarketHierarchyType.Region, Parent = nation };
            MarketHierarchy territory = new MarketHierarchy() { Code = "M3", CreatedBy = "Unit Test", Name = "Alexandria", Type = MarketHierarchyType.Territory, Parent = region };
            MarketHierarchy route = new MarketHierarchy() { Code = "M4", CreatedBy = "Unit Test", Name = "Richmond Hwy", Type = MarketHierarchyType.Route, Parent = territory };

            UnitOfWork.MarketHierarchies.Add(route);
            UnitOfWork.MarketHierarchies.Add(territory);
            UnitOfWork.MarketHierarchies.Add(region);
            UnitOfWork.MarketHierarchies.Add(nation);

            UnitOfWork.Complete();

            var all = UnitOfWork.MarketHierarchies.Find(x => x.Id > 0);

            Assert.AreEqual(4, all.Count);
            
            MarketHierarchy dbNation = UnitOfWork.MarketHierarchies.Get(nation.Id);

            Assert.IsNotNull(dbNation);

            UnitOfWork.MarketHierarchies.Remove(dbNation);

            UnitOfWork.Complete();

            all = UnitOfWork.MarketHierarchies.Find(x => x.Id > 0);

            Assert.AreEqual(3, all.Count);

            MarketHierarchy dbRegion = UnitOfWork.MarketHierarchies.Get(region.Id);

            Assert.IsNotNull(dbRegion);

            Assert.IsNull(dbRegion.Parent);
        }

        [TestMethod]
        public async Task DeleteMarketHierarchyAsync()
        {
            MarketHierarchy nation = new MarketHierarchy() { Code = "M1", CreatedBy = "Unit Test", Name = "US", Type = MarketHierarchyType.Nation };
            MarketHierarchy region = new MarketHierarchy() { Code = "M2", CreatedBy = "Unit Test", Name = "Virginia", Type = MarketHierarchyType.Region, Parent = nation };
            MarketHierarchy territory = new MarketHierarchy() { Code = "M3", CreatedBy = "Unit Test", Name = "Alexandria", Type = MarketHierarchyType.Territory, Parent = region };
            MarketHierarchy route = new MarketHierarchy() { Code = "M4", CreatedBy = "Unit Test", Name = "Richmond Hwy", Type = MarketHierarchyType.Route, Parent = territory };

            UnitOfWork.MarketHierarchies.Add(route);
            UnitOfWork.MarketHierarchies.Add(territory);
            UnitOfWork.MarketHierarchies.Add(region);
            UnitOfWork.MarketHierarchies.Add(nation);

            await UnitOfWork.CompleteAsync();

            var all = await UnitOfWork.MarketHierarchies.FindAsync(x => x.Id > 0);

            Assert.AreEqual(4, all.Count);

            MarketHierarchy dbNation = await UnitOfWork.MarketHierarchies.GetAsync(nation.Id);

            Assert.IsNotNull(dbNation);

            UnitOfWork.MarketHierarchies.Remove(dbNation);

            UnitOfWork.Complete();

            all = await UnitOfWork.MarketHierarchies.FindAsync(x => x.Id > 0);

            Assert.AreEqual(3, all.Count);

            MarketHierarchy dbRegion = await UnitOfWork.MarketHierarchies.GetAsync(region.Id);

            Assert.IsNotNull(dbRegion);

            Assert.IsNull(dbRegion.Parent);
        }

        [TestMethod]
        public async Task InsertMarketHierarchyAsync()
        {
            MarketHierarchy nation = new MarketHierarchy() { Code = "M1", CreatedBy = "Unit Test", Name = "US", Type = MarketHierarchyType.Nation };
            MarketHierarchy region = new MarketHierarchy() { Code = "M2", CreatedBy = "Unit Test", Name = "Virginia", Type = MarketHierarchyType.Region, Parent = nation };
            MarketHierarchy territory = new MarketHierarchy() { Code = "M3", CreatedBy = "Unit Test", Name = "Alexandria", Type = MarketHierarchyType.Territory, Parent = region };
            MarketHierarchy route = new MarketHierarchy() { Code = "M4", CreatedBy = "Unit Test", Name = "Richmond Hwy", Type = MarketHierarchyType.Route, Parent = territory };

            UnitOfWork.MarketHierarchies.Add(route);
            UnitOfWork.MarketHierarchies.Add(territory);
            UnitOfWork.MarketHierarchies.Add(region);
            UnitOfWork.MarketHierarchies.Add(nation);

            await UnitOfWork.CompleteAsync();

            MarketHierarchy dbRoute =await UnitOfWork.MarketHierarchies.GetAsync(route.Id);

            Assert.IsNotNull(dbRoute);
            Assert.IsNotNull(dbRoute.Parent);

            MarketHierarchy dbTerritory = dbRoute.Parent;

            Assert.AreEqual(dbTerritory.Code, territory.Code);
            Assert.AreEqual(dbTerritory.Name, territory.Name);
            Assert.AreEqual(dbTerritory.Type, territory.Type);

            MarketHierarchy dbRegion = dbTerritory.Parent;

            Assert.AreEqual(dbRegion.Code, region.Code);
            Assert.AreEqual(dbRegion.Name, region.Name);
            Assert.AreEqual(dbRegion.Type, region.Type);

            MarketHierarchy dbNation = dbRegion.Parent;

            Assert.AreEqual(dbNation.Code, nation.Code);
            Assert.AreEqual(dbNation.Name, nation.Name);
            Assert.AreEqual(dbNation.Type, nation.Type);
        }
    }
}

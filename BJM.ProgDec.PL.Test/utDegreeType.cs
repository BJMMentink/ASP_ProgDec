using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.PL.Test
{
    [TestClass]
    public class utDegreeType
    {
         protected ProgDecEntities dc;
        protected IDbContextTransaction transaction;
        [TestInitialize]
        public void Initialize()
        {
            dc = new ProgDecEntities();
            transaction = dc.Database.BeginTransaction();
        }
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            transaction = null;
        }
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblDegreeTypes.Count());
        }
        [TestMethod]
        public void InsertTest()
        {
            tblDegreeType entity = new tblDegreeType();
            entity.Id = 2;
            entity.Description = "Basket Weaving";
            entity.Id = -99;

            dc.tblDegreeTypes.Add(entity);

            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void UpdateTest()
        {
            tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault();

            entity.Description = "New Description";
            entity.Id = 1;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void DeleteTest() 
        { 
            tblDegreeType entity = dc.tblDegreeTypes.Where(e => e.Id == 1).FirstOrDefault();
            dc.tblDegreeTypes.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
        [TestMethod]
        public void LoadByIdTest()
        {
            tblDegreeType entity = dc.tblDegreeTypes.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}

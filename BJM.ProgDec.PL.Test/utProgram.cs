

namespace BJM.ProgDec.PL.Test
{
    [TestClass]
    public class utProgram
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
            Assert.AreEqual(16, dc.tblPrograms.Count());
        }
        [TestMethod]
        public void InsertTest()
        {
            tblProgram entity = new tblProgram();
            entity.DegreeTypeId = 2;
            entity.Description = "Basket Weaving";
            entity.Id = -99;

            dc.tblPrograms.Add(entity);

            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void UpdateTest()
        {
            tblProgram entity = dc.tblPrograms.FirstOrDefault();

            entity.Description = "New Description";
            entity.DegreeTypeId = 3;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void DeleteTest() 
        { 
            tblProgram entity = dc.tblPrograms.Where(e => e.Id == 4).FirstOrDefault();
            dc.tblPrograms.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}
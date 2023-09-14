namespace BJM.ProgDec.PL.Test
{
    [TestClass]
    public class utStudent
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
            Assert.AreEqual(5, dc.tblStudents.Count());
        }
        [TestMethod]
        public void InsertTest()
        {
            tblStudent entity = new tblStudent();
            entity.FirstName = "Hugo";
            entity.LastName = "Weaving";
            entity.StudentId = "123456824";
            entity.Id = -99;

            dc.tblStudents.Add(entity);

            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void UpdateTest()
        {
            tblStudent entity = dc.tblStudents.FirstOrDefault();

            entity.FirstName = "Hugo";
            entity.LastName = "Weaving";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void DeleteTest()
        {
            tblStudent entity = dc.tblStudents.Where(e => e.Id == 4).FirstOrDefault();
            dc.tblStudents.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.PL.Test
{
    [TestClass]
    public class utDeclaration
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
            Assert.AreEqual(4, dc.tblDeclarations.Count());
        }
        [TestMethod]
        public void InsertTest()
        {
            tblDeclaration entity = new tblDeclaration();
            entity.Id = -22;
            entity.ProgramId = -23;
            entity.StudentId = -24;
            entity.ChangeDate = DateTime.Now;
       

            dc.tblDeclarations.Add(entity);

            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void UpdateTest()
        {
            tblDeclaration entity = dc.tblDeclarations.FirstOrDefault();

            entity.ProgramId = 23;
            entity.StudentId = 24;


            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void DeleteTest()
        {
            tblDeclaration entity = dc.tblDeclarations.Where(e => e.Id == 4).FirstOrDefault();
            dc.tblDeclarations.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
        [TestMethod]
        public void LoadByIdTest()
        {
            tblDeclaration entity = dc.tblDeclarations.Where(e => e.Id == 4).FirstOrDefault();
            Assert.AreEqual(entity.Id, 4);
        }
    }
}

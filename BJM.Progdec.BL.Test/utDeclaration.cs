using BJM.ProgDec.BL;
using BJM.ProgDec.BL.Models;

namespace BJM.Progdec.BL.Test
{
    [TestClass]
    public class utDeclaration
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(4, DeclarationManager.Load().Count);
        }     
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            Declaration declaration = new Declaration
            {
                StudentId = 3,
                ProgramId = 4,
            };

            int results = DeclarationManager.Insert(declaration, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Declaration declaration = DeclarationManager.LoadById(3);
            declaration.StudentId = 2;
            int results = DeclarationManager.Update(declaration, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = DeclarationManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}
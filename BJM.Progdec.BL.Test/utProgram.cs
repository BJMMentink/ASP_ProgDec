using BJM.ProgDec.BL;
using BJM.ProgDec.BL.Models;

namespace BJM.Progdec.BL.Test
{
    [TestClass]
    public class utProgram
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(4, DeclarationManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            Program program = new Program
            {
                DegreeTypeId = 1,
            };
            int result = ProgramManager.Insert(program, true);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Program program = ProgramManager.LoadById(3);
            program.Description = "test";
            int result = ProgramManager.Update(program, true);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int result = ProgramManager.Delete(3, true);
            Assert.AreEqual(1, result);
        }
    }
}
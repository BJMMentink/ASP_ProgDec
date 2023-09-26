using BJM.ProgDec.BL;
using BJM.ProgDec.BL.Models;

namespace BJM.Progdec.BL.Test
{
    [TestClass]
    public class utDegreeType
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
            DegreeType degreeType = new DegreeType
            {
                Description = "test",
            };
            int result = DegreeTypeManager.Insert(degreeType, true);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void UpdateTest()
        {
            DegreeType degreeType = DegreeTypeManager.LoadById(3);
            degreeType.Description = "test";
            int result = DegreeTypeManager.Update(degreeType, true);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int result = DegreeTypeManager.Delete(3, true);
            Assert.AreEqual(1, result);
        }
    }
}
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
            List<DegreeType> items = DegreeTypeManager.Load();
            Assert.AreEqual(4, items.Count);
            Assert.AreEqual(9, items[2].Programs.Count());
        }
        [TestMethod]
        public void LoadById()
        {
            var item = DegreeTypeManager.LoadById(1);
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual(5, item.Programs.Count());
        }
        [TestMethod]
        public void InsertTest()
        {
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
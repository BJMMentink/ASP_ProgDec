namespace BJM.Progdec.BL.Test
{
    [TestClass]
    public class utDegreeType
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, DegreeTypeManager.Load().Count);
        }
    }
}
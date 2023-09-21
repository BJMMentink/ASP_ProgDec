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
    }
}
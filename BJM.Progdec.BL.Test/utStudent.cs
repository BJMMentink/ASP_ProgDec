using BJM.ProgDec.BL;

namespace BJM.Progdec.BL.Test
{
    [TestClass]
    public class utStudent
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(5, StudenManager.Load().Count);
        }
    }
}
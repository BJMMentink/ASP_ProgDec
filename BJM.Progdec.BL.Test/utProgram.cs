using BJM.ProgDec.BL;

namespace BJM.Progdec.BL.Test
{
    [TestClass]
    public class utProgram
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(16, ProgramManager.Load().Count);
        }
    }
}
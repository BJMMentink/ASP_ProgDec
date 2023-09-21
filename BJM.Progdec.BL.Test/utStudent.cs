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
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int result = StudenManager.Insert("Bale", "Organa", "456789123", ref id, true);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Student student = new Student
            {
                FirstName = "test",
                LastName = "test",
                StudentId = "test"
            };
            int result = StudenManager.Insert(student, true);
            Assert.AreEqual(1, result);
        }
    }
}
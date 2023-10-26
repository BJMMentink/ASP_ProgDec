using BJM.ProgDec.BL;
using BJM.ProgDec.BL.Models;

namespace BJM.Progdec.BL.Test
{
    [TestClass]
    public class utStudent
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(5, StudentManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int result = StudentManager.Insert("Bale", "Organa", "456789123", ref id, true);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void InsertTest2()
        {
            Student student = new Student
            {
                FirstName = "test",
                LastName = "test",
                StudentId = "test"
            };
            int result = StudentManager.Insert(student, true);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Student student = StudentManager.LoadById(3);
            student.FirstName = "test";
            int result = StudentManager.Update(student, true);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int result = StudentManager.Delete(3, true);
            Assert.AreEqual(1, result);
        }
    }
}
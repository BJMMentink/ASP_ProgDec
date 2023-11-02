namespace BJM.Progdec.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void LoginSucessfulTest()
        {
            Seed();
            Assert.IsTrue(UserManager.Login(new User { UserId = "bment", Password = "123Dev" }));
            Assert.IsTrue(UserManager.Login(new User { UserId = "bfoote", Password = "maple" }));
        }
        public void Seed()
        {
            UserManager.Seed();
        }
        [TestMethod]
        public void InsertTest()
        {

        }
        [TestMethod]
        public void LoginFailureNoUserId()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "", Password = "Dev123" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            
        }
        [TestMethod]
        public void LoginFailureBadPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "bfoote", Password = "Dev123" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureBadUserId()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "thing", Password = "Dev123" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureNoPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "bfoote", Password = "" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}

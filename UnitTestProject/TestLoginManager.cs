using NUnit.Framework;
using Refactoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestFixture]
    public class TestLoginManager
    {
        List<User> sampleUsers;
        FakeUserInterface ui = new FakeUserInterface();

        [SetUp]
        public void setUp()
        {
            sampleUsers = new List<User>();
            sampleUsers.Add(new User {Name = "Al", Bal = 1.0, Pwd = "lA"});
            sampleUsers.Add(new User {Name = "Beth", Bal = 2.0, Pwd = "hteB"});
            sampleUsers.Add(new User {Name = "Carl", Bal = 3.0, Pwd = "lraC"});
            sampleUsers.Add(new User {Name = "Dana", Bal = 4.0, Pwd = "anaD"});
            sampleUsers.Add(new User {Name = "Ed", Bal = 5.0, Pwd = "dE"});
            sampleUsers.Add(new User {Name = "Fara", Bal = 6.0, Pwd = "araF"});
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void testCannotBeCreatedWithoutUserList()
        {
            LoginManager loginManager = new LoginManager(null, ui);
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void testCannotBeCreatedWithoutUi()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, null);
        }

        [Test]
        public void testLoginAsksForAUserName()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse(null);
            loginManager.login();
            Assert.That(ui.getStringInputLabels(), Contains.Item("\r\nEnter Username:"));
        }

        [Test]
        public void testIfUserProvidesNullUserNameReturnNull()
        {
            LoginManager loginmanager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse(null);
            User user = loginmanager.login();
            Assert.Null(user);
        }

        [Test]
        public void testIfUserProvidesBlankUserNameReturnNull()
        {
            LoginManager loginmanager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("");
            User user = loginmanager.login();
            Assert.Null(user);
        }

        [Test]
        public void testIfUserProvidesBadUserNameTheyGetAnError()
        {
            LoginManager loginmanager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("NotARealUserName");
            ui.queueStringResponse("");
            User user = loginmanager.login();
            Assert.AreEqual("You entered an invalid user.", ui.getErrorText());
        }

        [Test]
        public void testIfUserProvidesBadUserNameTheyAreAskedAgain()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("NotARealUserName");
            ui.queueStringResponse("");
            loginManager.login();
            ICollection<string> inputLabels = ui.getStringInputLabels();
            Assert.AreEqual(2, inputLabels.Count);
            foreach (string inputLabel in inputLabels)
                Assert.AreEqual("\r\nEnter Username:", inputLabel);
        }
    }
}

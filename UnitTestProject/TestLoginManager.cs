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
        FakeUserInterface ui;

        [SetUp]
        public void setUp()
        {
            sampleUsers = new List<User>();
            sampleUsers.Add(new User {Name = "Al", Balance = 1.0, Password = "lA"});
            sampleUsers.Add(new User {Name = "Beth", Balance = 2.0, Password = "hteB"});
            sampleUsers.Add(new User {Name = "Carl", Balance = 3.0, Password = "lraC"});
            sampleUsers.Add(new User {Name = "Dana", Balance = 4.0, Password = "anaD"});
            sampleUsers.Add(new User {Name = "Ed", Balance = 5.0, Password = "dE"});
            sampleUsers.Add(new User {Name = "Fara", Balance = 6.0, Password = "araF"});
            sampleUsers.Add(new User {Name = "Carl", Balance = 3.0, Password = "IAmAlsoCarl"});
            sampleUsers.Add(new User { Name = "Beth", Balance = 2.0, Password = "hteB" });

            ui = new FakeUserInterface();
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
            Assert.That(ui.getStringInputLabels(), Contains.Item(Environment.NewLine + "Enter Username:"));
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
            User user = loginmanager.login();
            Assert.AreEqual("You entered an invalid user.", ui.getErrorText());
        }

        [Test]
        public void testIfUserProvidesBadUserNameTheyAreAskedAgain()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("NotARealUserName");
            loginManager.login();
            ICollection<string> inputLabels = ui.getStringInputLabels();
            Assert.AreEqual(2, inputLabels.Count);
            foreach (string inputLabel in inputLabels)
                Assert.AreEqual(Environment.NewLine + "Enter Username:", inputLabel);
        }

        [Test]
        public void testIfUserProvidesGoodUserNameTheyAreAskedForAPassword()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("Al");
            loginManager.login();
            Assert.That(ui.getStringInputLabels(), Contains.Item("Enter Password:"));
        }

        [Test]
        public void testIfUserProvidesGoodUserNameAndBadPasswordTheyGetAnError()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("Al");
            ui.queueStringResponse("FakePassword");
            loginManager.login();
            Assert.AreEqual("You entered an invalid password.", ui.getErrorText());
        }

        [Test]
        public void testIfUserProvidesBadUserNameTheyAreAskedToLoginAgain()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("Al");
            ui.queueStringResponse("FakePassword");
            loginManager.login();
            ICollection<string> inputLabels = ui.getStringInputLabels();
            Assert.AreEqual(3, inputLabels.Count);
            Assert.AreEqual(Environment.NewLine + "Enter Username:", inputLabels.ElementAt(0));
            Assert.AreEqual("Enter Password:", inputLabels.ElementAt(1));
            Assert.AreEqual(Environment.NewLine + "Enter Username:", inputLabels.ElementAt(2));
        }

        [Test]
        public void testIfUserProvidesGoodUserNameAndPasswordTheyGetThatUserBack()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("Carl");
            ui.queueStringResponse("lraC");
            User user = loginManager.login();
            Assert.AreSame(sampleUsers[2], user);
        }

        [Test]
        public void testBugfixCanLoginAsTheSixthUser()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("Fara");
            ui.queueStringResponse("araF");
            User user = loginManager.login();
            Assert.AreSame(sampleUsers[5], user);
        }

        [Test]
        public void testCanLoginAsTwoDifferentUsersWithSameNameButDifferentPassword()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("Carl");
            ui.queueStringResponse("lraC");
            User user = loginManager.login();
            Assert.AreSame(sampleUsers[2], user);

            ui.queueStringResponse("Carl");
            ui.queueStringResponse("IAmAlsoCarl");
            user = loginManager.login();
            Assert.AreSame(sampleUsers[6], user);
        }

        [Test]
        public void testBugfixIfTwoUsersHaveTheSameNameAndPasswordTheFirstOneIsRetrieved()
        {
            LoginManager loginManager = new LoginManager(sampleUsers, ui);
            ui.queueStringResponse("Beth");
            ui.queueStringResponse("hteB");
            User user = loginManager.login();
            Assert.AreSame(sampleUsers[1], user);
        }
    }
}

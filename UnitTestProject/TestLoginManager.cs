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
        [Test]
        [ExpectedException("System.ArgumentException")]
        public void testCannotBeCreatedWithoutUserList()
        {
            LoginManager loginManager = new LoginManager(null);
        }

        [Test]
        
    }
}

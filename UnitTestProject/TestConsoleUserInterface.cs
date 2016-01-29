using NUnit.Framework;
using Refactoring;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestFixture]
    public class TestConsoleUserInterface
    {
        [Test]
        public void testPrintBannerSendsBannerText()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                UserInterface ui = new ConsoleUserInterface();
                ui.displayBanner("This is my banner!");
                Assert.IsTrue(writer.ToString().Contains("This is my banner!"));
            }
        }

        [Test]
        public void testPrintBannerSendsUnderline()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                UserInterface ui = new ConsoleUserInterface();
                ui.displayBanner("This is my banner!");
                Assert.IsTrue(writer.ToString().Contains("------------------"));
            }
        }
    }
}

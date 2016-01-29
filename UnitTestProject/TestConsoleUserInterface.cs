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

        [Test]
        public void testGetStringInputFromUserSendsLabelText()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                using (var reader = new StringReader("Test\r\n"))
                {
                    Console.SetIn(reader);
                    UserInterface ui = new ConsoleUserInterface();
                    string userInput = ui.getStringInputFromUser("This is my label: ");
                    Assert.IsTrue(writer.ToString().Contains("This is my label: "));
                }
            }
        }

        [Test]
        public void testGetStringInputFromUserReturnsTheUserInput()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                using (var reader = new StringReader("Test\r\n"))
                {
                    Console.SetIn(reader);
                    UserInterface ui = new ConsoleUserInterface();
                    string userInput = ui.getStringInputFromUser("This is my label: ");
                    Assert.AreEqual("Test", userInput);
                }
            }
        }

        [Test]
        public void testPromptUserToExitIncludesText()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                using (var reader = new StringReader("\r\n"))
                {
                    Console.SetIn(reader);
                    UserInterface ui = new ConsoleUserInterface();
                    ui.promptUserToExit();
                    Assert.IsTrue(writer.ToString().Contains("\r\nPress Enter key to exit"));
                }
            }
        }

        // RFUTODO: It would be nice to test that the colour gets set and reset as part of the error/warning/notice calls, too
        // RFUTODO: We should also demonstrate that multi-string messages work

        [Test]
        public void testDisplayErrorIncludesText()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                 UserInterface ui = new ConsoleUserInterface();
                ui.displayError("ERRORERROR");
                Assert.IsTrue(writer.ToString().Contains("ERRORERROR"));
            }
        }

        [Test]
        public void testDisplayWarningIncludesText()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                UserInterface ui = new ConsoleUserInterface();
                ui.displayWarning("WARNINGWARNING");
                Assert.IsTrue(writer.ToString().Contains("WARNINGWARNING"));
            }
        }

        [Test]
        public void testDisplayNoticeIncludesText()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                UserInterface ui = new ConsoleUserInterface();
                ui.displayNotice("NOTICENOTICE");
                Assert.IsTrue(writer.ToString().Contains("NOTICENOTICE"));
            }
        }

    }
}

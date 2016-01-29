using Refactoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    public class FakeUserInterface : UserInterface
    {
        public void displayBanner(string bannerText)
        {
        }

        public string getStringInputFromUser(string labelText)
        {
            return null;
        }

        public void promptUserToExit()
        {
        }
    }
}

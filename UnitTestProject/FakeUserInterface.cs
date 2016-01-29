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
        private List<string> stringInputLabels = new List<string>();

        public void displayBanner(string bannerText)
        {
        }

        public string getStringInputFromUser(string labelText)
        {
            stringInputLabels.Add(labelText);
            return null;
        }

        public void promptUserToExit()
        {
        }

        public ICollection<string> getStringInputLabels()
        {
            return stringInputLabels;
        }
    }
}

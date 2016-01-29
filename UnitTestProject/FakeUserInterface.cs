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
        private Queue<string> stringResponses = new Queue<string>();

        public void displayBanner(string bannerText)
        {
        }

        public string getStringInputFromUser(string labelText)
        {
            stringInputLabels.Add(labelText);
            return stringResponses.Dequeue();
        }

        public void promptUserToExit()
        {
        }

        public ICollection<string> getStringInputLabels()
        {
            return stringInputLabels;
        }

        public void queueStringResponse(string response)
        {
            stringResponses.Enqueue(response);
        }
    }
}

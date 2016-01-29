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
        private string errorText;

        public void displayBanner(string bannerText)
        {
        }

        public string getStringInputFromUser(string labelText)
        {
            stringInputLabels.Add(labelText);
            return stringResponses.Any() ? stringResponses.Dequeue() : null;
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

        public void displayError(params string[] errorText) 
        {
            this.errorText = errorText[0];
        }

        public void displayWarning(params string[] warningText) { }

        public void displayNotice(params string[] noticeText) { }

        public string getErrorText()
        {
            return errorText;
        }
    }
}

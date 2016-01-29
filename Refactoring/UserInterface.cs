using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public interface UserInterface
    {
        void displayBanner(string bannerText);
        string getStringInputFromUser(string labelText);
        void promptUserToExit();
        void displayErrors(params string[] errorText);
        void displayWarnings(params string[] warningText);
        void displayNotices(params string[] noticeText);
    }
}

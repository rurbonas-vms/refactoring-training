using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class LoginManager
    {
        private UserInterface ui;

        public LoginManager(List<User> users, UserInterface ui) // TODO: Make this an IEnumerable once we've changed the code to remove the indexing operators
        {
            if (users == null)
                throw new ArgumentException();

            if (ui == null)
                throw new ArgumentException();

            this.ui = ui;
        }

        public void login()
        {
            // Prompt for user input
            string userName = ui.getStringInputFromUser("\r\nEnter Username:"); // TODO: Deal with the extra line we need to add here

        }
    }
}

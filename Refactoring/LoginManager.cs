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
        private IEnumerable<User> users;

        public LoginManager(IEnumerable<User> users, UserInterface ui)
        {
            if (users == null)
                throw new ArgumentException();

            if (ui == null)
                throw new ArgumentException();

            this.users = users;
            this.ui = ui;
        }

        public User login()
        {
            while (true) // RFUTODO: Temporary!
            {
                // Prompt for user input
                string userName = ui.getStringInputFromUser(Environment.NewLine + "Enter Username:"); // TODO: Deal with the extra line we need to add here

                if (string.IsNullOrEmpty(userName))
                    return null;

                if (users.Any(user => user.Name == userName))
                {
                    // Prompt for user input
                    string password = ui.getStringInputFromUser("Enter Password:");

                    User matchingUser = users.FirstOrDefault(user => user.Name == userName && user.Pwd == password);

                    if (matchingUser != null)
                        return matchingUser;

                    ui.displayError("You entered an invalid password.");
                }
                else
                {
                    ui.displayError("You entered an invalid user.");
                }
            }
        }
    }
}

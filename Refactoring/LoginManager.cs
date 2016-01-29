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

        private bool isUserNameValid(string userName)
        {
            return users.Any(user => user.Name == userName);
        }

        private User getUserByUserNameAndPassword(string userName, string password)
        {
            return users.FirstOrDefault(user => user.Name == userName && user.Password == password);
        }

        public User login()
        {
            while (true) // RFUTODO: Temporary!
            {
                // Prompt for user input
                string userName = ui.getStringInputFromUser(Environment.NewLine + "Enter Username:"); // TODO: Deal with the extra line we need to add here

                if (string.IsNullOrEmpty(userName))
                    return null;

                if (isUserNameValid(userName))
                {
                    string password = ui.getStringInputFromUser("Enter Password:");

                    User matchingUser = getUserByUserNameAndPassword(userName, password);

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

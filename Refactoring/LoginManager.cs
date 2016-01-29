using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class LoginManager
    {
        public LoginManager(List<User> users) // TODO: Make this an IEnumerable once we've changed the code to remove the indexing operators
        {
            if (users == null)
                throw new ArgumentException();
        }
    }
}

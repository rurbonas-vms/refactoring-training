using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class ConsoleUserInterface : UserInterface
    {
        public void displayBanner(string bannerText)
        {
            Console.WriteLine(bannerText);
            Console.WriteLine(new string('-', bannerText.Length));
        }

        public string getStringInputFromUser(string labelText)
        {
            Console.WriteLine();
            Console.WriteLine(labelText);
            return Console.ReadLine();
        }

        public void promptUserToExit()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter key to exit");
            Console.ReadLine();
        }
    }
}

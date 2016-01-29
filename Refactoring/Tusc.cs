using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class Tusc
    {
        private static void writeMessages(ConsoleColor color, params string[] messages)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            Console.WriteLine();
            foreach (string message in messages)
                Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Start(List<User> users, List<Product> products, UserInterface ui)
        {
            // Write welcome message
            ui.displayBanner("Welcome to TUSC");

            LoginManager loginManager = new LoginManager(users, ui);
            User loggedInUser = loginManager.login();

            if (loggedInUser == null)
            {
                ui.promptUserToExit();
                return;
            }

            // Show welcome message
            writeMessages(ConsoleColor.Green, "Login successful! Welcome " + loggedInUser.Name + "!");
                        
            // Show balance 
            Console.WriteLine();
            Console.WriteLine("Your balance is " + loggedInUser.Balance.ToString("C"));

            // Show product list
            while (true)
            {
                // Prompt for user input
                Console.WriteLine();
                Console.WriteLine("What would you like to buy?");
                for (int i = 0; i < 7; i++)
                {
                    Product prod = products[i];
                    Console.WriteLine(i + 1 + ": " + prod.Name + " (" + prod.Price.ToString("C") + ")");
                }
                Console.WriteLine(products.Count + 1 + ": Exit");

                // Prompt for user input
                string answer = ui.getStringInputFromUser("Enter a number:");
                int num = Convert.ToInt32(answer);
                num = num - 1; 

                // Check if user entered number that equals product count
                if (num == 7)
                {
                    // Write out new balance
                    string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                    File.WriteAllText(@"Data/Users.json", json);

                    // Write out new quantities
                    string json2 = JsonConvert.SerializeObject(products, Formatting.Indented);
                    File.WriteAllText(@"Data/Products.json", json2);


                    // Prevent console from closing
                    ui.promptUserToExit();
                    return;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("You want to buy: " + products[num].Name);
                    Console.WriteLine("Your balance is " + loggedInUser.Balance.ToString("C"));

                    // Prompt for user input
                    answer = ui.getStringInputFromUser("Enter amount to purchase:");
                    int qty = Convert.ToInt32(answer);

                    // Check if balance - quantity * price is less than 0
                    if (loggedInUser.Balance - products[num].Price * qty < 0)
                    {
                        writeMessages(ConsoleColor.Red, "You do not have enough money to buy that.");
                        continue;
                    }

                    // Check if quantity is less than quantity
                    if (products[num].Qty <= qty)
                    {
                        writeMessages(ConsoleColor.Red, "Sorry, " + products[num].Name + " is out of stock");
                        continue;
                    }

                    // Check if quantity is greater than zero
                    if (qty > 0)
                    {
                        // Balance = Balance - Price * Quantity
                        loggedInUser.Balance = loggedInUser.Balance - products[num].Price * qty;

                        // Quanity = Quantity - Quantity
                        products[num].Qty = products[num].Qty - qty;

                        writeMessages(ConsoleColor.Green, "You bought " + qty + " " + products[num].Name, "Your new balance is " + loggedInUser.Balance.ToString("C"));
                    }
                    else
                    {
                        // Quantity is less than zero
                        writeMessages(ConsoleColor.Yellow, "Purchase cancelled");
                    }
                }
            }
        }
    }
}

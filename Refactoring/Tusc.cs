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

        public static void Start(List<User> usrs, List<Product> prods, UserInterface ui)
        {
            // Write welcome message
            ui.displayBanner("Welcome to TUSC");

            // Login
            Login:
            bool loggedIn = false; // Is logged in?

            // Prompt for user input
            string name = ui.getStringInputFromUser("\r\nEnter Username:"); // TODO: Deal with the extra line we need to add here

            // Validate Username
            bool valUsr = false; // Is valid user?
            if (!string.IsNullOrEmpty(name))
            {
                for (int i = 0; i < 5; i++)
                {
                    User user = usrs[i];
                    // Check that name matches
                    if (user.Name == name)
                    {
                        valUsr = true;
                    }
                }

                // if valid user
                if (valUsr)
                {
                    // Prompt for user input
                    string pwd = ui.getStringInputFromUser("Enter Password:");

                    // Validate Password
                    bool valPwd = false; // Is valid password?
                    for (int i = 0; i < 5; i++)
                    {
                        User user = usrs[i];

                        // Check that name and password match
                        if (user.Name == name && user.Pwd == pwd)
                        {
                            valPwd = true;
                        }
                    }

                    // if valid password
                    if (valPwd == true)
                    {
                        loggedIn = true;

                        // Show welcome message
                        writeMessages(ConsoleColor.Green, "Login successful! Welcome " + name + "!");
                        
                        // Show remaining balance
                        double bal = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            User usr = usrs[i];

                            // Check that name and password match
                            if (usr.Name == name && usr.Pwd == pwd)
                            {
                                bal = usr.Bal;

                                // Show balance 
                                Console.WriteLine();
                                Console.WriteLine("Your balance is " + usr.Bal.ToString("C"));
                            }
                        }

                        // Show product list
                        while (true)
                        {
                            // Prompt for user input
                            Console.WriteLine();
                            Console.WriteLine("What would you like to buy?");
                            for (int i = 0; i < 7; i++)
                            {
                                Product prod = prods[i];
                                Console.WriteLine(i + 1 + ": " + prod.Name + " (" + prod.Price.ToString("C") + ")");
                            }
                            Console.WriteLine(prods.Count + 1 + ": Exit");

                            // Prompt for user input
                            string answer = ui.getStringInputFromUser("Enter a number:");
                            int num = Convert.ToInt32(answer);
                            num = num - 1; /* Subtract 1 from number
                            num = num + 1 // Add 1 to number */

                            // Check if user entered number that equals product count
                            if (num == 7)
                            {
                                // Update balance
                                foreach (var usr in usrs)
                                {
                                    // Check that name and password match
                                    if (usr.Name == name && usr.Pwd == pwd)
                                    {
                                        usr.Bal = bal;
                                    }
                                }

                                // Write out new balance
                                string json = JsonConvert.SerializeObject(usrs, Formatting.Indented);
                                File.WriteAllText(@"Data/Users.json", json);

                                // Write out new quantities
                                string json2 = JsonConvert.SerializeObject(prods, Formatting.Indented);
                                File.WriteAllText(@"Data/Products.json", json2);


                                // Prevent console from closing
                                ui.promptUserToExit();
                                return;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("You want to buy: " + prods[num].Name);
                                Console.WriteLine("Your balance is " + bal.ToString("C"));

                                // Prompt for user input
                                answer = ui.getStringInputFromUser("Enter amount to purchase:");
                                int qty = Convert.ToInt32(answer);

                                // Check if balance - quantity * price is less than 0
                                if (bal - prods[num].Price * qty < 0)
                                {
                                    writeMessages(ConsoleColor.Red, "You do not have enough money to buy that.");
                                    continue;
                                }

                                // Check if quantity is less than quantity
                                if (prods[num].Qty <= qty)
                                {
                                    writeMessages(ConsoleColor.Red, "Sorry, " + prods[num].Name + " is out of stock");
                                    continue;
                                }

                                // Check if quantity is greater than zero
                                if (qty > 0)
                                {
                                    // Balance = Balance - Price * Quantity
                                    bal = bal - prods[num].Price * qty;

                                    // Quanity = Quantity - Quantity
                                    prods[num].Qty = prods[num].Qty - qty;

                                    writeMessages(ConsoleColor.Green, "You bought " + qty + " " + prods[num].Name, "Your new balance is " + bal.ToString("C"));
                                }
                                else
                                {
                                    // Quantity is less than zero
                                    writeMessages(ConsoleColor.Yellow, "Purchase cancelled");
                                }
                            }
                        }
                    }
                    else
                    {
                        // Invalid Password
                        writeMessages(ConsoleColor.Red, "You entered an invalid password.");

                        goto Login;
                    }
                }
                else
                {
                    // Invalid User
                    writeMessages(ConsoleColor.Red, "You entered an invalid user.");

                    goto Login;
                }
            }

            // Prevent console from closing
            ui.promptUserToExit();
        }
    }
}

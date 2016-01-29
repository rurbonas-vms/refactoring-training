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
            ui.displayNotices("Login successful! Welcome " + loggedInUser.Name + "!");
                        
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
                    Product product = products[i];
                    Console.WriteLine(i + 1 + ": " + product.Name + " (" + product.Price.ToString("C") + ")");
                }
                Console.WriteLine(products.Count + 1 + ": Exit");

                // Prompt for user input
                string answer = ui.getStringInputFromUser("Enter a number:");
                int selectedProductIndex = Convert.ToInt32(answer);
                selectedProductIndex = selectedProductIndex - 1; 

                // Check if user entered number that equals product count
                if (selectedProductIndex == products.Count)
                {
                    // Write out new balance
                    string usersJSON = JsonConvert.SerializeObject(users, Formatting.Indented);
                    File.WriteAllText(@"Data/Users.json", usersJSON);

                    // Write out new quantities
                    string productsJSON = JsonConvert.SerializeObject(products, Formatting.Indented);
                    File.WriteAllText(@"Data/Products.json", productsJSON);

                    // Prevent console from closing
                    ui.promptUserToExit();
                    return;
                }
                else
                {
                    Product selectedProduct = products[selectedProductIndex];
                    Console.WriteLine();
                    Console.WriteLine("You want to buy: " + selectedProduct.Name);
                    Console.WriteLine("Your balance is " + loggedInUser.Balance.ToString("C"));

                    // Prompt for user input
                    answer = ui.getStringInputFromUser("Enter amount to purchase:");
                    int quantityToPurchase = Convert.ToInt32(answer);

                    // Check if balance - quantity * price is less than 0
                    if (loggedInUser.Balance - selectedProduct.Price * quantityToPurchase < 0)
                    {
                        ui.displayErrors("You do not have enough money to buy that.");
                        continue;
                    }

                    // Check if quantity is less than quantity
                    if (selectedProduct.Quantity <= quantityToPurchase)
                    {
                        ui.displayErrors("Sorry, " + selectedProduct.Name + " is out of stock");
                        continue;
                    }

                    // Check if quantity is greater than zero
                    if (quantityToPurchase > 0)
                    {
                        // Balance = Balance - Price * Quantity
                        loggedInUser.Balance = loggedInUser.Balance - selectedProduct.Price * quantityToPurchase;

                        // Quanity = Quantity - Quantity
                        selectedProduct.Quantity = selectedProduct.Quantity - quantityToPurchase;

                        ui.displayNotices("You bought " + quantityToPurchase + " " + selectedProduct.Name, "Your new balance is " + loggedInUser.Balance.ToString("C"));
                    }
                    else
                    {
                        // Quantity is less than zero
                        ui.displayWarnings("Purchase cancelled");
                    }
                }
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Refactoring
{
    public class Program
    {
        private static string USERS_FILE = @"Data/Users.json";
        private static string PRODUCTS_FILE = @"Data/Products.json";

        public static void Main(string[] args)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(USERS_FILE));
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(PRODUCTS_FILE));

            UserInterface ui = new ConsoleUserInterface();

            Tusc.Start(users, products, ui);

            File.WriteAllText(USERS_FILE, JsonConvert.SerializeObject(users, Formatting.Indented));
            File.WriteAllText(PRODUCTS_FILE, JsonConvert.SerializeObject(products, Formatting.Indented));
        }
    }
}

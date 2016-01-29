using NUnit.Framework;
using Refactoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestFixture]
    public class TestStore
    {
        List<Product> sampleProducts;
        FakeUserInterface ui;

        [SetUp]
        public void setUp()
        {
            sampleProducts = new List<Product>();
            sampleProducts.Add(new Product { Name = "Apple", Price = 1.0, Quantity = 1 });
            sampleProducts.Add(new Product { Name = "Banana", Price = 2.0, Quantity = 2 });
            sampleProducts.Add(new Product { Name = "Cookies", Price = 3.0, Quantity = 3 });
            sampleProducts.Add(new Product { Name = "Donut", Price = 4.0, Quantity = 4 });
            sampleProducts.Add(new Product { Name = "Eclair", Price = 5.0, Quantity = 5 });
            sampleProducts.Add(new Product { Name = "Frozen Yogurt", Price = 6.0, Quantity = 6 });
            sampleProducts.Add(new Product { Name = "Gelato", Price = 7.0, Quantity = 7 });
            sampleProducts.Add(new Product { Name = "Ham", Price = 8.0, Quantity = 8 });
            sampleProducts.Add(new Product { Name = "Icing", Price = 9.0, Quantity = 9 });

            ui = new FakeUserInterface();
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void testCannotBeCreatedWithoutProductList()
        {
            Store store = new Store(null, ui);
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void testCannotBeCreatedWithoutUi()
        {
            Store store = new Store(sampleProducts, null);
        }
    }
}

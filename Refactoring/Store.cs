using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class Store
    {
        public Store(List<Product> products, UserInterface ui)
        {
            if (products == null)
                throw new ArgumentException();

            if (ui == null)
                throw new ArgumentException();
        }
    }
}

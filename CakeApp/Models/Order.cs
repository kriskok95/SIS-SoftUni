using System;
using System.Collections.Generic;
using System.Text;

namespace CakeApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

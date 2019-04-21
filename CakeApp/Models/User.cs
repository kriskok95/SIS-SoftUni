using System;
using System.Collections.Generic;
using System.Text;
using Remotion.Linq.Clauses;

namespace CakeApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}

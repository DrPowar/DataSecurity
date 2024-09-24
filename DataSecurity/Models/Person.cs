using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSecurity.Models
{
    internal class Person
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Role Role { get; set; }

        public string? Password { get; set; }

        public bool IsBlocked { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSecurity.DTO
{
    internal record RegisterRequest(string Name, string? Password);
}

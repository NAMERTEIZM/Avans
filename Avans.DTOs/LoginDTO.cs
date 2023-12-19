using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int EmployeeID { get; set; }
        public int TitleID { get; set; }
        public string TitleName { get; set; }

    }
}

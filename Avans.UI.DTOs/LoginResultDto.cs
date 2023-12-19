using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.UI.DTOs
{
    public class LoginResultDto
    {
        public EmployeeDTO Employee { get; set; }
        public string Token { get; set; }
    }
}

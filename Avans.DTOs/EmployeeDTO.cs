﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.DTOs
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public int TitleId { get; set; }
        public string Password { get; set; }
        public string TitleName { get; set; }
    }
}
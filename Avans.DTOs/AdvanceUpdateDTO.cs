using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.DTOs
{
    public class AdvanceUpdateDTO
    {
        public int ID { get; set; }
        public int StatusID { get; set; }
        public int AdvanceAmount { get; set; }
        public int TransactorID { get; set; }
        public bool isApproved { get; set; }

    }
}

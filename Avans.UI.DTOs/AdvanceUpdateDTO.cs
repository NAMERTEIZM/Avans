using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.UI.DTOs
{
    public class AdvanceUpdateDTO
    {
        public int ID { get; set; }
        public int StatusID { get; set; }
        public int ApprovedAmount { get; set; }
        public int TransactorID { get; set; }
        public bool isApproved { get; set; }
        public DateTime? DeterminedPaymentDate { get; set; }
        public int ReceiptNo { get; set; }
        public int EmployeeID { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.UI.DTOs
{
    public class AdvancesPendingApprovalSelectDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TitleName { get; set; }
        public string BusinessUnitName { get; set; }
        public string StatusName { get; set; }
        public DateTime RequestDate { get; set; }
        public int AdvanceAmount { get; set; }
        public DateTime DesiredDate { get; set; }
        public string ProjectName { get; set; }
        public int ApprovedAmount { get; set; }
        public string AdvanceDescription { get; set; }
        public DateTime Date { get; set; }
        public int TransactorID { get; set; }
        public int StatusID { get; set; }
        public DateTime DeterminedPaymentDate { get; set; }
        public int EmployeeID { get; set; }

    }
}

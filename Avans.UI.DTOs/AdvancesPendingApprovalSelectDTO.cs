using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.DTOs
{
    public class AdvancesPendingApprovalSelectDTO
    {
        public string Name { get; set; }
        public string TitleName { get; set; }
        public string BusinessUnitName { get; set; }
        public string StatusName { get; set; }
        public DateTime RequestDate { get; set; }
        public int AdvanceAmount { get; set; }
        public DateTime DesiredDate { get; set; }
        public string ProjectName { get; set; }
    }
}

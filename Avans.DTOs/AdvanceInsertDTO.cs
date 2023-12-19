using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.DTOs
{
    public class AdvanceInsertDTO
    {
        [Column("ID")]

        public int ID { get; set; }
        [Column("AdvanceAmount")]

        public decimal AdvanceAmount { get; set; }
        [Column("AdvanceDescription")]
        public string AdvanceDescription { get; set; }
        [Column("ProjectID")]

        public int ProjectID { get; set; }
        [Column("DesiredDate")]

        public DateTime DesiredDate { get; set; }
        [Column("EmployeeID")]

        public int EmployeeID { get; set; }
        public DateTime RequestDate { get; set; }


    }
}

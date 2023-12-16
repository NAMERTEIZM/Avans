using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Models.Entities
{
    [Table("Advance")]
    public class AdvanceReal
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

        [Column("StatusID")]
        public int StatusID { get; set; }

        [Column("PaymentID")]
        public int PaymentID { get; set; }

        [Column("RequestDate")]
        public DateTime RequestDate { get; set; }

        [Column("ProjectName")]
        public string ProjectName { get; set; }

        [Column("StatusName")]
        public string StatusName { get; set; }


        [Column("DeterminedPaymentDate")]
        public DateTime DeterminedPaymentDate { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}

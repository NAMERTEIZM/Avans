using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Avans.DTOs
{
    [Table("Advance")]

    public class AdvanceDTO
    {
        [Key]
        public int ID { get; set; }
        public int StatusID { get; set; }
        public int AdvanceID { get; set; }
        public int TransactorID { get; set; }
        public int PaymentID { get; set; }
        public decimal ApprovedAmount { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }
        public decimal AdvanceAmount { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DesiredDate { get; set; }
        public string ProjectName { get; set; }
        public string StatusName { get; set; }
        public string Name { get; set; }
        public string TitleName { get; set; }
        public DateTime DeterminedPaymentDate { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string ReceiptNo { get; set; }

        public bool isRefundReceipt { get; set; }

    }
}

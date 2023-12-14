﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Avans.UI.DTOs
{
    [Table("AdvanceHistory")]
    public class AdvanceSelectDTO
    {
        [Key]
        public int ID { get; set; } 
        public int StatusID { get; set; } 
        public int AdvanceID { get; set; } 
        public int TransactorID { get; set; } 
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
        public bool isRefundReceipt { get; set; }
    }
}
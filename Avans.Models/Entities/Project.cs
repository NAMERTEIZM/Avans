using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Models.Entities
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int ID { get; set; }

        [Column("ProjectName")]
        
        public string ProjectName { get; set; }

        [Column("ProjectDescription")] 
        public string ProjectDescription { get; set; }

        [Column("EndDate")] 
        public DateTime EndDate { get; set; }

        [Column("StartingDate")] 
        public DateTime StartingDate { get; set; }
    }
}

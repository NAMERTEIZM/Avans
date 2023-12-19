using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.UI.DTOs
{
    [Table("Project")]
    public class ProjectSelectDTO
    {
        [Key]
        public int ID { get; set; }

        [Column("ProjectName")]

        public string ProjectName { get; set; }
        public int EmployeeID { get; set; }


    }
}

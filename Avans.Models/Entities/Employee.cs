using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Models.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            //AdvanceHistories = new HashSet<AdvanceHistory>();
            //EmployeeProjects = new HashSet<EmployeeProject>();
            //InverseUpperEmployee = new HashSet<Employee>();
            //Payments = new HashSet<Payment>();
            //Receipts = new HashSet<Receipt>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? TitleID { get; set; }
        public int? UpperEmployeeId { get; set; }

        //public virtual BusinessUnit BusinessUnit { get; set; }
        //public virtual Title Title { get; set; }
        //public virtual Employee UpperEmployee { get; set; }
        //public virtual ICollection<Advance> Advances { get; set; }
        //public virtual ICollection<AdvanceHistory> AdvanceHistories { get; set; }
        //public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
        //public virtual ICollection<Employee> InverseUpperEmployee { get; set; }
        //public virtual ICollection<Payment> Payments { get; set; }
        //public virtual ICollection<Receipt> Receipts { get; set; }
    }
}

using Avans.DTOs;
using Avans.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.DAL.Abstract
{
    public interface IAuthDAL
    {

        Task<bool> Register(Employee emplpoyee);
        Task<Employee> Login(string mail);

        Task<EmployeeDTO> GetUserInfo(string mail);
    }
}

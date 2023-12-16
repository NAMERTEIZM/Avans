using Avans.Core.Contexts;
using Avans.DAL.Abstract;
using Avans.DTOs;
using Avans.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.DAL.Concrete
{
    public class AuthDAL : IAuthDAL
    {
        private readonly ConnectionHelper _helper;
        private IDbConnection _dbconnection;
        public AuthDAL(ConnectionHelper helper, IDbConnection dbconnection)
        {
            _helper = helper;
            _dbconnection = dbconnection;   
            _dbconnection.Open();
        }

        public async Task<bool> Register(Employee emplpoyee)
        {
            using var conn = _helper.CreateConnection();
            conn.Open();
            var query = @"INSERT INTO Employee(Name, Surname, Email, TitleID, PasswordHash, PasswordSalt) VALUES(@Name, @Surname, @Email, @TitleID, @PasswordHash, @PasswordSalt)";
            var parameters = new DynamicParameters();

            parameters.Add("@Name", emplpoyee.Name, DbType.String);
            parameters.Add("@Surname", emplpoyee.Surname, DbType.String);
            parameters.Add("@Email", emplpoyee.Email, DbType.String);
            parameters.Add("@TitleID", emplpoyee.TitleId, DbType.Int32);
            parameters.Add("@PasswordHash", emplpoyee.PasswordHash, DbType.Binary);
            parameters.Add("@PasswordSalt", emplpoyee.PasswordSalt, DbType.Binary);

            var rowaffected = await conn.ExecuteAsync(query, parameters);
            return rowaffected > 0;
        }

        public async Task<Employee> Login(string mail)
        {
            string query = @"SELECT * FROM Employee
                      WHERE Email=@Email";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", mail, DbType.String);
            using var conn = _helper.CreateConnection();
            var user = await conn.QueryFirstOrDefaultAsync<Employee>(query, parameters);

            return user;
        }

        public async Task<EmployeeDTO> GetUserInfo(string mail)
        {
            string query = @"SELECT Name,Surname,Email,TitleName FROM Employee inner join Title on Title.ID=Employee.TitleID WHERE Email=@Email";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", mail, DbType.String);
            using var conn = _helper.CreateConnection();
            var employeeInfo = await conn.QueryAsync<Employee, Title, EmployeeDTO>(query, (employee, title) =>
            {
                var employeeDTO = new EmployeeDTO
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    SurName = employee.Surname,
                    TitleName = title.TitleName
                };

                return employeeDTO;
            }, new { Email = mail }, splitOn: "TitleName");

            return employeeInfo.FirstOrDefault();

        }
    }
}

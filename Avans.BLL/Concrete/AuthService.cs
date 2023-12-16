using Avans.Core.Contexts;
using Avans.Core.Mappers;
using Avans.DAL.Abstract;
using Avans.DTOs;
using Avans.DTOs.ApiResult;
using Avans.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Avans.BLL.Concrete
{
    public class AuthService
    {
        private readonly IAuthDAL _authDAL;
        private readonly TokenHelper _tokenHelper;
        private readonly MyMapper<EmployeeDTO,Employee> _mapper;

        public AuthService(IAuthDAL authDAL, TokenHelper tokenHelper, MyMapper<EmployeeDTO, Employee> mapper)
        {
            _authDAL = authDAL;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }
        public async Task<EmployeeDTO> GetUserInfo(string email)
        {
            var employee = await _authDAL.GetUserInfo(email);
            return employee;
        }
        public async Task<string> Login(string email, string password)
        {
            var user = await _authDAL.Login(email);

            if (user != null && CheckPassword(password, user.PasswordSalt, user.PasswordHash))
                return _tokenHelper.CreateToken(user);

            return null;
        }

        public async Task<ApiResult<ApiResultDTO>> Register(EmployeeDTO employeeDTO, string password)
        {
            var employee = _mapper.Map<EmployeeDTO, Employee>(employeeDTO);
            var user = await _authDAL.Login(employee.Email);
            if (user != null)
            {
                return new ApiResult<ApiResultDTO>
                {
                    IsSuccess = false,
                    Data = new ApiResultDTO() { Result = false },
                    Message = "Kullanıcı zaten var"
                };
            }

            byte[] passHash, passSalt;
            CreatePassword(password, out passHash, out passSalt);
            employee.PasswordHash = passHash;
            employee.PasswordSalt = passSalt;

            var state = await _authDAL.Register(employee);

            return new ApiResult<ApiResultDTO>
            {
                IsSuccess = state,
                Data = new ApiResultDTO() { Result = state }
            };
        }

        private bool CheckPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var _passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < _passwordHash.Length; i++)
                {
                    if (passwordHash[i] != _passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void CreatePassword(string password, out byte[] pasHash, out byte[] passSalt)
        {
            {
                using (var hmac = new HMACSHA512())
                {
                    pasHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    passSalt = hmac.Key;
                }
            }
        }
    }
}

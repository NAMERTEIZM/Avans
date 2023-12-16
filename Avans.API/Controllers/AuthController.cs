using Avans.DTOs.ApiResult;
using Avans.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using Avans.BLL.Concrete;

namespace Avans.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authservice;
        private readonly IConfiguration _conf;

        public AuthController(AuthService authservice, IConfiguration conf)
        {
            _authservice = authservice;
            _conf = conf;
        }

        [HttpPost("~/api/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var loginResultDto = new ApiResult<LoginResultDto>();

            var isThereUser = await _authservice.Login(dto.Email, dto.Password);

            if (isThereUser == null)
            {
                return NotFound(dto.Email);
            }
            var userInfo = await _authservice.GetUserInfo(dto.Email);

            var issuer = _conf["JwtIssuer"];
            var audience = _conf["JwtAudience"];

            var desc = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddMinutes(20),
                Subject = new ClaimsIdentity(new Claim[]
                {
             new Claim(ClaimTypes.Name,userInfo.Email),
             new Claim(ClaimTypes.Role,userInfo.TitleName),

                }),
                Issuer = issuer,
                Audience = audience,
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["MySecurityKey"])), SecurityAlgorithms.HmacSha256)
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(desc);
            var userTokenValue = tokenhandler.WriteToken(token);
            loginResultDto.Data = new LoginResultDto()
            {
                Token = userTokenValue,
                Employee = new EmployeeDTO
                {
                    Email = userInfo.Email,
                    TitleId = userInfo.TitleId,
                    Name = userInfo.Name,
                    Password = userInfo.Password,
                    SurName = userInfo.SurName,
                    TitleName = userInfo.TitleName,
                }
            };
            return Ok(loginResultDto);
        }

        [HttpPost("~/api/register")]
        public async Task<IActionResult> Register([FromBody] EmployeeDTO dto)
        {
            var registeredUser = await _authservice.Register(dto, dto.Password);
            return Ok(registeredUser);
        }
    }
}

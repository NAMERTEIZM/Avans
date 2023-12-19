using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Avans.APIConnection;
using Avans.UI.DTOs;

namespace Avans.UI.Controllers
{
    public class AccountController : Controller
    {
        ApiRequestService _apirequestservice;
        TitleService _titleservice;
        public AccountController(ApiRequestService apirequestservice, TitleService titleService)
        {
            _apirequestservice = apirequestservice;
            _titleservice = titleService;
        }
        public IActionResult Login()
        {
            return View(new EmployeeDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(EmployeeDTO dto)
        {

            var result = await _apirequestservice.GetToken(dto);

            if (result != null)
            {
                //cookie

                HttpContext.Response.Cookies.Append("token", result.Token, new CookieOptions()
                {
                    Expires = System.DateTimeOffset.Now.AddMinutes(20),
                    Domain = "Avans"
                });
                

                var claims = new List<Claim>()
                {
                    //burayı doldurrsss
                    new Claim(ClaimTypes.Name,result.Employee.Name),
                    new Claim(ClaimTypes.Email,result.Employee.Email),
                    new Claim(ClaimTypes.Role,result.Employee.TitleName),
                    new Claim(ClaimTypes.Surname,result.Employee.SurName),
                    new Claim(ClaimTypes.Anonymous,result.Employee.TitleID.ToString()),
                    new Claim(ClaimTypes.NameIdentifier,result.Employee.ID.ToString()),

                };


                var userIdentity = new ClaimsIdentity(claims, "login");
                var userpri = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userpri);


                //çıkış için
                //await HttpContext.SignOutAsync(,);

                return RedirectToAction("Index","Advance");


                // return RedirectToAction("VeriEkle", new {dto=dto});
            }
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> Register()
        {
            var valueForTitle = await _titleservice.GetTitle();
            ViewBag.title = valueForTitle;


            return View(new EmployeeDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(EmployeeDTO dto)
        {
            var donendeger = await _apirequestservice.Register(dto);

            //ggirişe yönlendir

            if (donendeger)
            {
                TempData["kullaniciDurumu"] = "Kullanıcı başarıyla kayıt edilmiştir.Lütfen giriş yapınız.";

                return RedirectToAction("Login","Account");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }
}

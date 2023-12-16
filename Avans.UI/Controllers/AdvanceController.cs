using Avans.APIConnection;
using Avans.UI.DTOs;
using Avans.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using System.Threading.Tasks;

namespace Avans.UI.Controllers
{
    public class AdvanceController : Controller
    {
        AdvanceService _api;
        ProjectService _projectService;
        public AdvanceController(AdvanceService  api, ProjectService projectService)
        {
            _api = api;
            _projectService = projectService;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AdvanceHistory()
        {
            var donendeger = await _api.GetAdvance();
            ViewBag.sonuc = donendeger;

            //TempData["sonuc"] = donendeger;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdvance(AdvanceInsertDTO dto)
        {

            var donendeger = await _api.AddAdvance(dto);
            TempData["sonuc"] = donendeger;
            return RedirectToAction("AdvanceHistory");
        }
        [HttpGet]
        public async Task<IActionResult> AddAdvance()
        {

            var donendeger = await _projectService.GetProject();
            ViewBag.project = donendeger;
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AdvanceHistory(AdvanceSelectDTO dto)
        //{

        //    return RedirectToAction("Index");
        //}
    }
}

﻿using Avans.APIConnection;
using Avans.UI.DTOs;
using Avans.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Avans.UI.Controllers
{
    [Authorize]
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
            var employeeID = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var donendeger = await _api.GetAdvance(employeeID);
            ViewBag.sonuc = donendeger;

            //TempData["sonuc"] = donendeger;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AdvanceDetail(int id)
        {
            
            var donendeger = await _api.GetAdvanceByID(id);

            ViewBag.sonuc = donendeger;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdvancePendingApprovalDetail(int id)
        {

            var donendeger = await _api.GetAdvanceForPendingApprovalDetailByID(id);

            ViewBag.sonuc = donendeger;
            return View();
        }
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdvancePendingApprovalDetail(AdvanceUpdateDTO dto)
        {

            var donendeger = await _api.ApproveAdvance(dto);
            TempData["sonuc"] = donendeger;
            return RedirectToAction("AdvanceHistory");
        }

        [HttpGet]
        public async Task<IActionResult> FinancialMannagerDetail(int id)
        {

            var donendeger = await _api.GetAdvanceForPendingApprovalDetailByID(id);

            ViewBag.sonuc = donendeger;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinancalManagerSetPaymentDate(AdvanceUpdateDTO dto)
        {

            var donendeger = await _api.ApproveAdvance(dto);
            TempData["sonuc"] = donendeger;
            return RedirectToAction("AdvanceHistory");
        }

        [HttpGet]
        public async Task<IActionResult> AccountantDetail(int id)
        {

            var donendeger = await _api.GetAdvanceForPendingApprovalDetailByID(id);

            ViewBag.sonuc = donendeger;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountantDetail(AdvanceUpdateDTO dto)
        {

            var donendeger = await _api.ApproveAdvance(dto);
            TempData["sonuc"] = donendeger;
            return RedirectToAction("AdvanceHistory");
        }

        [HttpGet]
        public async Task<IActionResult> AdvancePendingApproval()
        {
            var statusid = Convert.ToInt32(User.FindFirst(ClaimTypes.Anonymous)?.Value);
            var donendeger = await _api.GetAdvancePending(statusid);
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

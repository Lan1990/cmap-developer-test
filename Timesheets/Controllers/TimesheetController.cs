﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Timesheets.Models;
using Timesheets.Services;

namespace Timesheets.Controllers
{
    public class TimesheetController : Controller
    {
        private ITimesheetService _timesheetService;
        private readonly ICsvService _csvService;

        public TimesheetController(ITimesheetService timesheetService, ICsvService csvService)
        {
            _timesheetService = timesheetService;
            _csvService = csvService;
        }

        public IActionResult Index()
        {
            var  timesheets = _timesheetService.GetAll().OrderBy(x=>x.TotalHours);
            ViewBag.Timesheets = timesheets;
            return View();
        }

        [HttpPost]
        public ActionResult Index(TimesheetEntry timesheetEntry)
        {
            var timesheet = new Timesheet()
            {
                TimesheetEntry = timesheetEntry,
                TotalHours = timesheetEntry.Hours
            };

            _timesheetService.Add(timesheet);

            ViewBag.Timesheets = _timesheetService.GetAll();

            return View();
        }
        [HttpGet]
        public IActionResult Csv()
        {
            var timesheets = _timesheetService.GetAll();
            var csv = _csvService.CreateCSV(timesheets);
            if (csv == string.Empty)
            {
                return NoContent();
            }
            return Content(csv, "text/csv");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
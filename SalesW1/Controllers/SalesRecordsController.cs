using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesW1.Services;

namespace SalesW1.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordsService;

        public SalesRecordsController(SalesRecordService salesRecordsService)
        {
            _salesRecordsService = salesRecordsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleFind(DateTime? dtOne, DateTime? dtTwo)
        {
            //if (!dtOne.HasValue)
            //{
            //    dtOne = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //}
            //if (!dtTwo.HasValue)
            //{
            //    dtTwo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //}

            //ViewData["minDate"] = dtOne.Value.ToString("yyyy-MM-dd");
            //ViewData["maxDate"] = dtTwo.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordsService.FindByDateAsync(dtOne, dtTwo);
            return View(result);
        }

        public async Task<IActionResult> GroupFind(DateTime? dtOne, DateTime? dtTwo)
        {
            //if (!dtOne.HasValue)
            //{
            //    dtOne = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //}
            //if (!dtTwo.HasValue)
            //{
            //    dtTwo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //}

            //ViewData["minDate"] = dtOne.Value.ToString("yyyy-MM-dd");
            //ViewData["maxDate"] = dtTwo.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordsService.FindByDateGroupAsync(dtOne, dtTwo);
            return View(result);
        }


    }
}
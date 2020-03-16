using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesW1.Controllers
{
    public class SalesRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleFind()
        {
            return View();
        }

        public IActionResult GroupFind()
        {
            return View();
        }


    }
}
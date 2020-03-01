using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SalesW1.Models;

namespace SalesW1.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departs = new List<Department>();

            departs.Add(new Department { Id = 1, Name = "Eletronics" });
            departs.Add(new Department { Id = 2, Name = "Tools" });

            return View(departs);
        }
    }
}
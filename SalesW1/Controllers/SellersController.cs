using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesW1.Data;
using SalesW1.Models;
using SalesW1.Services;
using SalesW1.Models.ViewModels;
using SalesW1.Services.Exceptions;
using System.Diagnostics;

namespace SalesW1.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // Sincronous
        //public IActionResult Index()
        //{
        //    var list = _sellerService.FindAll();
        //    return View(list);
        //}

        // Async
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }


        public async Task<IActionResult> Create()
        {
            var departaments = await _departmentService.ListAllAsync();
            var viewModel = new SellerFormViewModel { Department = departaments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                // To enforce Model validation, in case JS is disable
                var departments = await _departmentService.ListAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Department = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id doesn't exist" });
            }

            var seller = await _sellerService.FindByIdAsync(id.Value);
            
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller is Not found" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id doesn't exist" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller is Not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id doesn't exist" });
            }

            var seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller is Not found" });
            }

            List<Department> myDepartments = await _departmentService.ListAllAsync();
            SellerFormViewModel sellerViewModel = new SellerFormViewModel { Seller = seller, Department = myDepartments  };
            return View(sellerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                // To enforce Model validation, in case JS is disable
                var departments = await _departmentService.ListAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Department = departments};
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            //catch (NotFoundException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}
            //catch (DbConcurrencyException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}

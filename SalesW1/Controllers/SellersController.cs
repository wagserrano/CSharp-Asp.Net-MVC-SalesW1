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
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departaments = _departmentService.ListAll();
            var viewModel = new SellerFormViewModel { Department = departaments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id doesn't exist" });
            }

            var seller = _sellerService.FindById(id.Value);
            
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller is Not found" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id doesn't exist" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller is Not found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id doesn't exist" });
            }

            var seller = _sellerService.FindById(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller is Not found" });
            }

            List<Department> myDepartments = _departmentService.ListAll();
            SellerFormViewModel sellerViewModel = new SellerFormViewModel { Seller = seller, Department = myDepartments  };
            return View(sellerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
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
        // Assincronous
        //// GET: Sellers   
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Seller.ToListAsync());
        //}

        //// GET: Sellers/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var seller = await _context.Seller
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (seller == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(seller);
        //}

        //// GET: Sellers/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Sellers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Email,BirthDate,BaseSalary")] Seller seller)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(seller);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(seller);
        //}

        //// GET: Sellers/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var seller = await _context.Seller.FindAsync(id);
        //    if (seller == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(seller);
        //}

        //// POST: Sellers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,BirthDate,BaseSalary")] Seller seller)
        //{
        //    if (id != seller.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(seller);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SellerExists(seller.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(seller);
        //}

        //// GET: Sellers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var seller = await _context.Seller
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (seller == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(seller);
        //}

        //// POST: Sellers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var seller = await _context.Seller.FindAsync(id);
        //    _context.Seller.Remove(seller);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SellerExists(int id)
        //{
        //    return _context.Seller.Any(e => e.Id == id);
        //}
    }
}

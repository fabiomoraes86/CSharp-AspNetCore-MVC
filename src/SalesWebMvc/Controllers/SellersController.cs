using Api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _service;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService service, DepartmentService departmentService)
        {
            _service = service;
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_service.FindAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel
            {
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _service.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteView(int? id)
        {
            return View(_service.DeleteView(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            return View(_service.Details(id));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return View(_service.Edit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            _service.UpdateSeller(id, seller);
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Error(string message)
        //{
        //    var viewModel = new ErrorViewModel
        //    {
        //        Message = message,
        //        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        //    };

        //    return View(viewModel);
        //}

    }
}

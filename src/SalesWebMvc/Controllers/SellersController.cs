using Api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System.Diagnostics;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            return  View(await _service.FindAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel
            {
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel
            {
                Seller = seller,
                Departments = departments
            };

            await _service.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await _service.DeleteViewAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return View(await _service.DetailsAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await _service.EditAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel
                {
                    Seller = seller,
                    Departments = departments
                };
                return View(seller);
            }
                

            await _service.UpdateSellerAsync(id, seller);
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

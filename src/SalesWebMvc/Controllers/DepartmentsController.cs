using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Interfaces;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View(_service.GetDepartments());
        }
    }
}

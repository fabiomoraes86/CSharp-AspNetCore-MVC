using SalesWebMvc.Models;
using System.Collections.Generic;

namespace SalesWebMvc.Interfaces
{
    public interface IDepartmentService
    {
        ICollection<Department> GetDepartments();
    }
}

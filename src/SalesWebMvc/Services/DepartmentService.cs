using SalesWebMvc.Interfaces;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;

namespace SalesWebMvc.Services
{
    public class DepartmentService : IDepartmentService
    {
        public ICollection<Department> GetDepartments()
        {
            var list = new List<Department>();
            list.Add(new Department { Id = 1, Name = "Eletronics" });
            list.Add(new Department { Id = 2, Name = "Fashion" });

            return list;
        }
    }
}

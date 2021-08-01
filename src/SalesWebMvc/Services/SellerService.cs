using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;
        private readonly DepartmentService _department;
        private readonly SellerService _service;

        public SellerService(SalesWebMvcContext context, DepartmentService department)
        {
            _context = context;
            _department = department;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
           var sellerId = _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);

            if (sellerId == null)
                throw new NotFoundException($"Id not found");

            return sellerId;
        }

        public Seller DeleteView(int? id)
        {
            if (id == null)
                throw new NotImplementedException();

            var obj = FindById(id.Value);
            if (obj == null)
                throw new NotImplementedException();

            return obj;
        }

        public void Delete(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);

            _context.SaveChanges();
        }

        public Seller Details(int? id)
        {
            if (id == null)
                throw new NotImplementedException();

            var obj = FindById(id.Value);
            if (obj == null)
                throw new NotImplementedException();

            return obj;
        }

        public SellerFormViewModel Edit(int? id)
        {
            if (id == null)
                throw new NotFoundException("Id not found");

            var obj = FindById(id.Value);

            var departments = new List<Department>();
            departments = _department.FindAll();

            SellerFormViewModel viewModel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = departments
            };

            return viewModel;
        }

        public Seller UpdateSeller(int id, Seller obj)
        {
            if (id != obj.Id)
                throw new Exception("Bad Request");

            if (!_context.Seller.Any(x => x.Id == obj.Id))
                throw new NotFoundException("Id not found");

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }

            return obj;


        }

    }
}

using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;
        private readonly DepartmentService _department;

        public SellerService(SalesWebMvcContext context, DepartmentService department)
        {
            _context = context;
            _department = department;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            var sellerId = await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);

            if (sellerId == null)
                throw new NotFoundException($"Id not found");

            return sellerId;
        }

        public async Task<Seller> DeleteViewAsync(int? id)
        {
            if (id == null)
                throw new NotImplementedException();

            var obj = await FindByIdAsync(id.Value);
            if (obj == null)
                throw new NotImplementedException();

            return obj;
        }

        public async Task DeleteAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);

            await _context.SaveChangesAsync();
        }

        public async Task<Seller> DetailsAsync(int? id)
        {
            if (id == null)
                throw new NotImplementedException();

            var obj = await FindByIdAsync(id.Value);
            if (obj == null)
                throw new NotImplementedException();

            return obj;
        }

        public async Task<SellerFormViewModel> EditAsync(int? id)
        {
            if (id == null)
                throw new NotFoundException("Id not found");

            var obj = await FindByIdAsync(id.Value);

            var departments = new List<Department>();
            departments = await _department.FindAllAsync();

            SellerFormViewModel viewModel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = departments
            };

            return viewModel;
        }

        public async Task<Seller> UpdateSellerAsync(int id, Seller obj)
        {
            if (id != obj.Id)
                throw new Exception("Bad Request");

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
                throw new NotFoundException("Id not found");

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }

            return obj;

        }

    }
}

using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;
        private readonly SellerService _service;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
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
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);
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

    }
}

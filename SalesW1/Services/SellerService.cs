using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesW1.Data;
using SalesW1.Models;
using Microsoft.EntityFrameworkCore;
using SalesW1.Services.Exceptions;

namespace SalesW1.Services
{
    public class SellerService
    {

        private readonly SalesW1Context _context;

        public SellerService(SalesW1Context context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            // Prevent FK error
            // obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller mySeller)
        {
            if (!_context.Seller.Any(x => x.Id == mySeller.Id))
            {
                throw new NotFoundException("Id not found!");
            }
            try
            {
                _context.Update(mySeller);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message) ;
            }

        }
    }
}

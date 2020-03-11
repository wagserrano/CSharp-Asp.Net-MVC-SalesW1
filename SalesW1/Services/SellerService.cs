using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesW1.Data;
using SalesW1.Models;

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
    }
}

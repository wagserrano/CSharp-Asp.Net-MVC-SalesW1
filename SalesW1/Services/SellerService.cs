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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public void Insert(Seller obj)
        {
            // Prevent FK error
            // obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);                  // Ocurrency in memory
            await _context.SaveChangesAsync();  // This component effectively occurs async
        }

        public Seller FindById(int id)
        {
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
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

        public async Task UpdateAsync(Seller mySeller)
        {
            bool testSeller = await _context.Seller.AnyAsync(x => x.Id == mySeller.Id);
            
            if (!testSeller)
            {
                throw new NotFoundException("Id not found!");
            }
            try
            {
                _context.Update(mySeller);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}

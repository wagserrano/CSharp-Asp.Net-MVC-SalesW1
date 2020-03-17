using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesW1.Data;
using SalesW1.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesW1.Services
{
    public class SalesRecordService
    {
        private readonly SalesW1Context _context;

        public SalesRecordService(SalesW1Context context)
        {
            _context = context;
        }

        public async Task<List<SalesRecords>> FindByDateAsync(DateTime? startDate, DateTime? finishDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            
            if (startDate.HasValue)
            {
                result = result.Where(x => x.Date >= startDate.Value);
            }

            if (finishDate.HasValue)
            {
                result = result.Where(x => x.Date <= finishDate);
            }

            return await result
                .Include(x => x.Seller)             // Join
                .Include(x => x.Seller.Department)  // Join
                .OrderByDescending(x => x.Date)     // OrderBy
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecords>>> FindByDateGroupAsync(DateTime? startDate, DateTime? finishDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (startDate.HasValue)
            {
                result = result.Where(x => x.Date >= startDate.Value);
            }

            if (finishDate.HasValue)
            {
                result = result.Where(x => x.Date <= finishDate);
            }

            return await result
                .Include(x => x.Seller)             // Join
                .Include(x => x.Seller.Department)  // Join
                .OrderByDescending(x => x.Date)     // OrderBy
                .GroupBy(x => x.Seller.Department)  // Group By
                .ToListAsync();
        }



    }
}

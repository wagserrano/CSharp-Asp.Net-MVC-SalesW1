using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesW1.Data;
using SalesW1.Models;

namespace SalesW1.Services
{
    public class DepartmentService
    {
        private readonly SalesW1Context _context;

        public DepartmentService(SalesW1Context context)
        {
            _context = context;
        }

        public List<Department> ListAll()
        {
            //return _context.Department.ToList();
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

        public async Task<List<Department>> ListAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}

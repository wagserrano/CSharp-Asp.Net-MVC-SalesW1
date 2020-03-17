using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesW1.Models;
using SalesW1.Models.Enums;

namespace SalesW1.Data
{
    public class SeedindService
    {
        private SalesW1Context _context;

        public SeedindService(SalesW1Context context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Checking if there are registers
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return; // Database has been populated
            }

            Department d1 = new Department(1, "Eletronics");
            Department d2 = new Department(2, "Tools");
            Department d3 = new Department(3, "Computers");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Wag Serrano", "wagserrano@gmail.com", new DateTime(1993, 11, 16), 10000.0, d3);
            Seller s2 = new Seller(2, "Cam Serrano", "wagserrano@gmail.com", new DateTime(2000, 9, 24), 8500.0, d1);
            Seller s3 = new Seller(3, "Ryan Serrano", "wagserrano@gmail.com", new DateTime(2000, 11, 16), 5000.0, d2);
            Seller s4 = new Seller(4, "Renzo Serrano", "wagserrano@gmail.com", new DateTime(2000, 11, 16), 10000.0, d2);
            Seller s5 = new Seller(5, "Ralph Serrano", "wagserrano@gmail.com", new DateTime(2000, 11, 16), 10000.0, d4);
            Seller s6 = new Seller(6, "Rickson Serrano", "wagserrano@gmail.com", new DateTime(2000, 11, 16), 10000.0, d4);

            //SalesRecord sr1 = new SalesRecord(1, new DateTime(2020, 9, 21), 10000.0, Models.Enums.SaleStatus.Billed, s1);
            SalesRecords sr1 = new SalesRecords(1, new DateTime(2020, 9, 21), 10000.0, SaleStatus.Billed, s1);
            SalesRecords sr2 = new SalesRecords(2, new DateTime(2020, 9, 21), 17000.0, SaleStatus.Billed, s2);
            SalesRecords sr3 = new SalesRecords(3, new DateTime(2020, 9, 21), 8000.0, SaleStatus.Billed, s3);
            SalesRecords sr4 = new SalesRecords(4, new DateTime(2020, 9, 21), 6500.0, SaleStatus.Billed, s4);
            SalesRecords sr5 = new SalesRecords(5, new DateTime(2020, 9, 21), 12000.0, SaleStatus.Billed, s5);
            SalesRecords sr6 = new SalesRecords(6, new DateTime(2020, 9, 21), 9000.0, SaleStatus.Billed, s6);

            _context.Department.AddRange(d1, d2, d3, d4);

            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);

            _context.SalesRecord.AddRange(sr1, sr2, sr3, sr4, sr5, sr6);

            _context.SaveChanges();
        }
    }
}

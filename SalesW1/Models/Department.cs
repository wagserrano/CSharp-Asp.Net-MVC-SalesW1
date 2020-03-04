using System;
using System.Collections.Generic;
using System.Linq;
using SalesW1.Models;

namespace SalesW1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {

        }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller sell)
        {
            Sellers.Add(sell);
        }

        public double TotalSales(DateTime dtstart, DateTime dtend)
        {
            return Sellers.Sum(seller => seller.SalesAmount(dtstart, dtend));
        }
    }
}

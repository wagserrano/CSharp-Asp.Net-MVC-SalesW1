using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesW1.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Department { get; set; }
    }
}

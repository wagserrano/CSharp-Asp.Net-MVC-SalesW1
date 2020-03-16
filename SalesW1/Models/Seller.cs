using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesW1.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be beetween {2} and {1}")]
        // Element zero = Property Name, element 2 = First parameter (StringLength(60)), element 1  = Second parameter StringLength(??, MinimumLength = 3 )
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        // [Required(ErrorMessage = "{0} required")]
        [Range(1000.00, 50000.00, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name="Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecords> Sales { get; set; } = new List<SalesRecords>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecords slrec)
        {
            Sales.Add(slrec);
        }

        public void DelSales(SalesRecords slrec)
        {
            Sales.Remove(slrec);
        }

        public double SalesAmount(DateTime dtstart, DateTime dtend)
        {
            return Sales.Where(sr => sr.Date >= dtstart && sr.Date <= dtend).Sum(sr => sr.Amount);
        }
    }
}

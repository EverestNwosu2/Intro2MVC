using System;
using System.ComponentModel.DataAnnotations;

namespace Intro2MVC.Models
{
    [Serializable]
    public class Employee
    {
        [Required]
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int Age { get; set; }
        public string NationalInsuranceNumber { get; set; }
        public string Gender {get; set; }
        public string Address { get; set; }
        public double MonthlySalary {get; set; }

    }
}
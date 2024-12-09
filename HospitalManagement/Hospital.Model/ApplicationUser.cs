using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public DateTime DOB {  get; set; }
        public String Specialist { get; set; }
        public Department Department { get; set; }
        [NotMapped]
        public ICollection<Appointment> Appointments { get; set; }
        [NotMapped]
        public ICollection<Payroll> Payrolls { get; set;}

    }
}

namespace Hospital.Models
{
    public enum Gender
    {
       Male,Female,Other
    }
}

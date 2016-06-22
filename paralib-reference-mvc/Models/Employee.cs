using System;
using System.ComponentModel.DataAnnotations;
using com.paralib.DataAnnotations;

namespace com.paralib.reference.mvc.Models
{
    public class Employee
    {
        [Required]
        [StringLength(3)]
        public string Name { get; set; } = "Dude";

        [ParaType(ParaTypes.Email)]
        public string Email { get; set; } = "joe@itrg.org";

        public int Age { get; set; } = 32;
        public decimal Wage { get; set; }  = 30.5M;
        public DateTime Birthdate { get; set; } = DateTime.Now;
    }
}
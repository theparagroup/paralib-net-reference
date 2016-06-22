using System;
using com.paralib;
using com.paralib.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace com.paralib.reference.console
{

    class Employee
    {
        //[StringLength(2)]
        //[MinLength(20, ErrorMessage = "too small")]
        //[MaxLength(2)]
        public string FirstName { get; set; }

        //[Required]
        //[Display(Name = "Last Name")]
        //[String(3)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [ParaType(ParaTypes.Email)]
        public string Email { get; set; }

        [ParaType(ParaTypes.Zip4)]
        public string Zip { get; set; }


    }

    class Program
    {
        static void Main(string[] args)
        {
            var e = new Employee();
            e.Zip = "12345-1234";
            e.Email = "no";

            var errors = ObjectValidator.Validate(e);

            if (errors!=null)
            {
                foreach (var r in errors)
                {
                    Console.WriteLine($"\t{r.MemberName} - {r.ErrorMessage} ");
                }
            }
            else
            {
                Console.WriteLine("valid");
            }


        }

    }
}

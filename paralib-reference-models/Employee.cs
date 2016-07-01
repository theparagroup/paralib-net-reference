using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using com.paralib;
using com.paralib.DataAnnotations;

namespace com.paralib.reference.models
{
    public partial class Employee
    {
        public int Id { get; set; }

        [Required]
        [ParaType(ParaTypes.Name)]
        public string Name { get; set; }

        [Required]
        [ParaType(ParaTypes.Email)]
        public string Email { get; set; }

        public int CompanyId { get; set; }

        public EmployeeTypes EmployeeType { get; set; }
    }

}


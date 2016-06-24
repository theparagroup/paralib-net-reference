using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using com.paralib;
using com.paralib.DataAnnotations;

namespace com.paralib.reference.models
{
    public partial class Company
    {
        public int Id { get; set; }

        [Required]
        [ParaType(ParaTypes.Name)]
        public string Name { get; set; }

    }

}


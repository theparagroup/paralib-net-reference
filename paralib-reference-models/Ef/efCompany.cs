using System;
using System.Collections.Generic;


namespace com.paralib.reference.models.Ef
{
    public class EfCompany : Company
    {
        public virtual List<EfEmployee> Users { get; set; }
    }
}

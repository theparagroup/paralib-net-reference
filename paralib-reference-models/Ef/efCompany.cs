using System;
using System.Collections.Generic;


namespace com.paralib.reference.models.Ef
{
    public class efCompany : Company
    {
        public virtual List<efEmployee> Users { get; set; }
    }
}

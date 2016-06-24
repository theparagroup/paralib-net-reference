using System;

namespace com.paralib.reference.models.Ef
{
    public class efEmployee : Employee
    {
        public virtual efCompany Company { get; set; }
    }
}

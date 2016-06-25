using System;

namespace com.paralib.reference.models.Ef
{
    public class EfEmployee : Employee
    {
        public virtual EfCompany Company { get; set; }
    }
}

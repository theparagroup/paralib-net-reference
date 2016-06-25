using System;
using System.Data.Entity;
using com.paralib.Dal.Ef;
using com.paralib.reference.models.Ef;

namespace com.paralib.reference.models
{
    [DbConfigurationType(typeof(EfConfiguration))]
    public class DbContext:EfContext
    {
        public DbSet<EfCompany> Companies { get; set; }
        public DbSet<EfEmployee> Employees { get; set; }
    }
}

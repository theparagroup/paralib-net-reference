using System;
using System.Data.Entity;
using com.paralib.Dal.Ef;

namespace com.paralib.reference.models.Ef
{
    public class DbContext:EfContext
    {
        public DbSet<EfCompany> Companies { get; set; }
        public DbSet<EfEmployee> Employees { get; set; }
    }
}

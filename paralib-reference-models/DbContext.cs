using System;
using System.Data.Entity;
using com.paralib.Dal.Ef;
using com.paralib.reference.models.Ef;

namespace com.paralib.reference.models
{
    [DbConfigurationType(typeof(efConfiguration))]
    public class DbContext:efContext
    {
        public DbSet<efCompany> Companies { get; set; }
        public DbSet<efEmployee> Employees { get; set; }
    }
}

using System;
using System.Linq;
using com.paralib;
using com.paralib.Dal.Utils;
using com.paralib.reference.models.Ef;

namespace com.paralib.reference.console
{

    class Program
    {
        static ILog _log = Paralib.GetLogger("root");

        static void Main(string[] args)
        {
            try
            {
                Paralib.Initialize();
                Go();
            }
            catch (Exception e)
            {
                _log.Fatal(e);
                throw;
            }

        }

        static void Go()
        {

            using (var db = new DbContext())
            {
                var q = from e in db.Employees
                        where e.EmployeeType.Name=="Regular" && e.Company.Name=="ACME"
                        select e;

                foreach (var e in q.ToList())
                {
                    Console.WriteLine(e.Email +$" [{e.EmployeeType.Name}]"+ " [" + e.Company.Name + "]");
                }
            }

        }

    }
}

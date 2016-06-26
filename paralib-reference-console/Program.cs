using System;
using System.Linq;
using com.paralib;
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
                        select e;

                foreach (var e in q.ToList())
                {
                    Console.WriteLine(e.Email + " [" + e.Company.Name + "]<br />");
                }
            }

        }

    }
}

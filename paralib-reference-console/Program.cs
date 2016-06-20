using System;
using com.paralib;

namespace paralib_reference_console
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Threading.Thread.CurrentPrincipal = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity("joe"),new string[] { });

            Paralib.Initialize();

            Console.WriteLine($"logging enabled = {Paralib.Configuration.Logging.Enabled}");
            Console.WriteLine($"logging debug = {Paralib.Configuration.Logging.Debug}");
            Console.WriteLine($"logging level = {Paralib.Configuration.Logging.Level}");

            foreach (var l in Paralib.Configuration.Logging.Logs)
            {
                Console.WriteLine($"\tLOG: name={l.Name} type={l.LogType} loggertype={l.LoggerType}");
            }

            var log = Paralib.GetLogger("foo");
            log.Debug();
            log.Info();
            log.Warn();
            log.Error();
            log.Fatal();

        }
    }
}

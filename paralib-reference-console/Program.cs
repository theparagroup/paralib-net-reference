using System;
using com.paralib;

namespace paralib_reference_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Paralib.Configure += Paralib_Configure;

            System.Threading.Thread.CurrentPrincipal = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity("joe"),new string[] { });

            Paralib.Initialize();
            Paralib.RaiseConfigureEvent();
            

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

        private static void Paralib_Configure(com.paralib.Configuration.Settings settings)
        {
            settings.Logging.Logs.Add(new com.paralib.Logging.Log("joe", com.paralib.Logging.LogTypes.Database) { Table = "joe", Pattern = "%.100logger", Fields = "logger" });
        }
    }
}

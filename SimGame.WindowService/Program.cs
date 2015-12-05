using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SimGame.WindowService
{
    class Program
    {
        static void Main(string[] args)
        {App_Start.EntityFrameworkProfilerBootstrapper.PreStart();

            Console.WriteLine("Introduction Service");
            try
            {
                const string name = "SimGameService";
                const string description = "SimGame Service";
                var host = HostFactory.New(configuration =>
                {
                    configuration.Service<Host>(callback =>
                    {
                        callback.ConstructUsing(s => new Host());
                        callback.WhenStarted(service => service.Start());
                        callback.WhenStopped(service => service.Stop());
                    });
                    configuration.SetDisplayName(name);
                    configuration.SetServiceName(name);
                    configuration.SetDescription(description);
                    configuration.RunAsLocalService();
                });
                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SimGame Service fatal exception. " + ex.Message);
            }
        }
    }
}


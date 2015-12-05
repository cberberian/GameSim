using log4net.Config;

namespace SimGame.WebApi
{
    public class LoggingConfig
    {
        public static void ConfigureLogging()
        {
            XmlConfigurator.Configure();
        }
    }
}
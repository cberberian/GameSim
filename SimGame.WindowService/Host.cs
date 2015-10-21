using System;
using System.ServiceModel;
using SimGame.Service;

namespace SimGame.WindowService
{
    internal class Host
    {
        private ServiceHost _service;

        public Host()
        {
            _service = new ServiceHost(typeof(SimGameService));
        }

        public void Start()
        {
            Console.WriteLine("Starting services...");
            _service.Open();
            Console.WriteLine("Started!");
        }

        public void Stop()
        {
            Console.WriteLine("Stopping services...");
            try
            {
                if (_service != null)
                {
                    if (_service.State == CommunicationState.Opened)
                    {
                        _service.Close();
                    }
                }
                Console.WriteLine("Stopped!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not stop: " + ex.Message);
            }
        }
    }
}
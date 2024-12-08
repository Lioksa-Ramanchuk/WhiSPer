using System;
using System.ServiceModel;

namespace WcfSimplexHost
{
    internal class Program
    {
        static void Main()
        {            
            using (ServiceHost host = new ServiceHost(typeof(WcfSimplex.Simplex)))
            {
                host.Open();
                Console.WriteLine($"Simplex service is running");
                
                Console.WriteLine();
                Console.WriteLine("Press Enter to terminate the service.");
                Console.ReadLine();
            }
        }
    }
}
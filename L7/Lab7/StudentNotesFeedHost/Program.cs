﻿using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using WsRamSyndicationService;

namespace StudentNotesFeedHost
{
    internal class Program
    {
        static void Main()
        {
            using (WebServiceHost host = new WebServiceHost(typeof(StudentNotesFeed)))
            {
                foreach (var endpoint in host.Description.Endpoints)
                {
                    endpoint.Behaviors.Add(new CorsBehavior());
                }

                host.Open();
                Console.WriteLine($"WsRamSyndication host is running");

                Console.WriteLine();
                Console.WriteLine("Press Enter to terminate the service.");
                Console.ReadLine();
            }
        }
    }
}

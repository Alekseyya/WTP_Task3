using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Services.ClientService)))
            {
                host.Open();
                Console.WriteLine(DateTime.Now.ToString());
                host.Close();
                Console.ReadLine();
            }
        }
    }
}

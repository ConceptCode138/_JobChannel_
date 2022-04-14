using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

    internal class TraceService : ITraceService
    {
        string ITraceService.TraceOnFileTXT(string message)
        {
            string machineName = Dns.GetHostName();
            string ipAddress = Dns.GetHostEntry(machineName).AddressList[0].ToString();

            return $"[{DateTime.Now}] - [{machineName}] - [{ipAddress}] -> {message}";
        }

        void ITraceService.TraceOnServer(string message)
        {
            Console.WriteLine($"{DateTime.Now} - {message}");
        }
    }
}

using SRR.UIP.HW8.LandCalculator.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.DAL.Storages
{
    public class FileLogStorage : ILogStorage
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine($"{"FileLogStorage",20}:\t{message}");
        }
    }
}

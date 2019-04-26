using SRR.UIP.HW8.LandCalculator.BLL.Services;
using SRR.UIP.HW8.LandCalculator.DAL.Storages;
using SRR.UIP.HW8.LandCalculator.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.UIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeStorages();
            StaticInjector.Logger.Info("Program is started");
            
            new UIConsoleInteractor(new LandAreaCalculator()).Start();
            
            Console.ReadLine();
        }

        private static void InitializeStorages()
        {
            StaticInjector.Logger.LogStorages.Add(new ConsoleLogStorage());
            StaticInjector.Logger.LogStorages.Add(new FileLogStorage());
        }
    }
}

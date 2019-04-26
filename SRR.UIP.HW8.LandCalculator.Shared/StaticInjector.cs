using SRR.UIP.HW8.LandCalculator.Shared.Interfaces;
using SRR.UIP.HW8.LandCalculator.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.Shared
{
    public static class StaticInjector
    {
        public static ILogger Logger { get; } = new Logger();
    }
}

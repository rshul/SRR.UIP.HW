using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.Shared.Interfaces
{
    public interface ILogger
    {
        List<ILogStorage> LogStorages { get; }

        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Debug(string message);
        void Fatal(string message);
    }
}

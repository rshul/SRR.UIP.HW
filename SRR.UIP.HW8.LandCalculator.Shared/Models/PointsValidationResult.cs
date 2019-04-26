using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.Shared.Models
{
    public class PointsValidationResult
    {
        public bool ArePointsValid { get; set; }

        public string Message { get; set; }

        public PointsMistakes Mistakes { get; set; }
    }

    [Flags]
    public enum PointsMistakes:byte
    {
        Nothing  = 0,
        NotEnoughUniquePoints = 1,
        CrossedShape = 2,
        NotClosedShape = 4,
    }
}

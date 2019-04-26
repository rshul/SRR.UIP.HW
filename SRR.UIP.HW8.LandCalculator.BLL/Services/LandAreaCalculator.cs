using SRR.UIP.HW8.LandCalculator.Shared;
using SRR.UIP.HW8.LandCalculator.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.BLL.Services
{
    public class LandAreaCalculator : ILandCalculator
    {
        public long CalculateLandArea(List<Point> points, bool isAltAppr = true)
        {
            string calculationMode = isAltAppr ? "Main Calculation" : "Control calculation";
            StaticInjector.Logger.Info(calculationMode);
            long landArea = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int nextIndex = (i == points.Count - 1) ? 0 : i + 1;
                int prevIndex = (i == 0) ? points.Count - 1 : i - 1;
                long par1 = isAltAppr ? points[i].X : points[i].Y;
                long par2_1 = isAltAppr ? points[nextIndex].Y : points[prevIndex].X;
                long par2_2 = isAltAppr ? points[prevIndex].Y : points[nextIndex].X;
                landArea += par1 * (par2_1 - par2_2);
            }
            long result = (long)Math.Abs(landArea / 2);
            return result;
        }
    }
}

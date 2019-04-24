﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.Shared.Interfaces
{
    public interface ILandCalculator
    {
        int CalculateLandArea(List<Point> points);
    }

}

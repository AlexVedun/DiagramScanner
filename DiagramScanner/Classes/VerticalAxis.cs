﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace DiagramScanner.Classes
{
    class VerticalAxis: Axis
    {
        public VerticalAxis(Color color, double thickness) :base(color, thickness)
        {
            line.X1 = 100;
            line.Y1 = 0;
            line.X2 = 100;
            line.Y2 = 100;
        }

        public void SetX(double x)
        {
            line.X1 = x;
            line.X2 = x;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiagramScanner.Classes
{
    class HorizontalScaleMarker : Axis
    {
        public HorizontalScaleMarker(Canvas canvas, Color color, double thickness) : base(canvas, color, thickness)
        {
            line.X1 = 20;
            line.Y1 = 100 - 5;
            line.X2 = 20;
            line.Y2 = 100 + 5;
        }

        public void SetX(double x)
        {
            line.X1 = x;
            line.X2 = x;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiagramScanner.Classes
{
    class VerticalAxis: Axis
    {
        public VerticalAxis(Canvas canvas, Color color, double thickness, Axis marker = null) :base(canvas, color, thickness)
        {
            line.X1 = 100;
            line.Y1 = 0;
            line.X2 = 100;
            line.Y2 = 100;

            if (marker != null)
            {
                scaleMarker = marker;
            }
            else
            {
                scaleMarker = null;
            }
        }

        public void SetX(double x)
        {
            line.X1 = x;
            line.X2 = x;
            if (scaleMarker != null)
            {
                scaleMarker.X1 = line.X1 - 5;
                scaleMarker.X2 = line.X1 + 5;
            }
        }
    }
}

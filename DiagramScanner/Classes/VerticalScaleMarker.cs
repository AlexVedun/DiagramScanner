using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiagramScanner.Classes
{
    class VerticalScaleMarker : Axis
    {
        public VerticalScaleMarker(Canvas canvas, Color color, double thickness) : base(canvas, color, thickness)
        {
            line.X1 = 100 - 5;
            line.Y1 = 20;
            line.X2 = 100 + 5;
            line.Y2 = 20;
        }

        public void SetY(double y)
        {
            line.Y1 = y;
            line.Y2 = y;
        }
    }
}

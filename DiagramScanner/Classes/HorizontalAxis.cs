using System;
using System.Collections.Generic;
using System.Text;

namespace DiagramScanner.Classes
{
    class HorizontalAxis: Axis
    {
        public HorizontalAxis():base()
        {
            line.X1 = 0;
            line.Y1 = 100;
            line.X2 = 100;
            line.Y2 = 100;
        }

        public void SetY(double y)
        {
            line.Y1 = y;
            line.Y2 = y;
        }
    }
}

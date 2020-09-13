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
        public VerticalAxis(Canvas canvas, Color color, double thickness, bool marker) :base(canvas, color, thickness)
        {
            line.X1 = 100;
            line.Y1 = 0;
            line.X2 = 100;
            line.Y2 = 100;

            if (marker)
            {
                scaleMarker = new Line
                {
                    X1 = line.X1 - 5,
                    Y1 = 20,
                    X2 = line.X1 + 5,
                    Y2 = 20,
                    Stroke = new SolidColorBrush(color),
                    StrokeThickness = thickness,
                    Visibility = Visibility.Hidden
                };
                canvas.Children.Add(scaleMarker);
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

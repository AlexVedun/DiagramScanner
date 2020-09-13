using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiagramScanner.Classes
{
    class HorizontalAxis: Axis
    {
        public HorizontalAxis(Canvas canvas, Color color, double thickness, bool marker) :base(canvas, color, thickness)
        {
            line.X1 = 0;
            line.Y1 = 100;
            line.X2 = 100;
            line.Y2 = 100;

            if (marker)
            {
                scaleMarker = new Line
                {
                    X1 = 20,
                    Y1 = line.Y1 - 5,
                    X2 = 20,
                    Y2 = line.Y1 + 5,
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

        public void SetY(double y)
        {
            line.Y1 = y;
            line.Y2 = y;
            if (scaleMarker != null)
            {
                scaleMarker.Y1 = line.Y1 - 5;
                scaleMarker.Y2 = line.Y1 + 5;
            }            
        }
    }
}

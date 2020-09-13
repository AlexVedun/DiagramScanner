using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiagramScanner.Classes
{
    class Axis
    {
        protected Line line;
        protected Line scaleMarker;       

        public double X1
        {
            get => line.X1;
            set => line.X1 = value;
        }
        public double X2
        {
            get => line.X2;
            set => line.X2 = value;
        }
        public double Y1
        {
            get => line.Y1;
            set => line.Y1 = value;
        }
        public double Y2
        {
            get => line.Y2;
            set => line.Y2 = value;
        }

        public Axis(Canvas canvas, Color color, double thickness)
        {
            line = new Line
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = thickness,
                Visibility = Visibility.Hidden
            };
            canvas.Children.Add(line);
        }

        public void Show()
        {
            line.Visibility = Visibility.Visible;
            if (scaleMarker != null)
            {
                scaleMarker.Visibility = Visibility.Visible;
            }
        }

        public void Hide()
        {
            line.Visibility = Visibility.Hidden;
            if (scaleMarker != null)
            {
                scaleMarker.Visibility = Visibility.Hidden;
            }
        }

        

        
    }
}

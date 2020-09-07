using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiagramScanner.Classes
{
    class Scanner
    {
        private HorizontalAxis AxisX;
        private VerticalAxis AxisY;
        public Scanner DiagramScanner { get; set; }
        public Image DiagramImage { get; set; }
        public Canvas MainCanvas { get; set; }
        public bool IsAxisXMove { get; set; }
        public bool IsAxisYMove { get; set; }

        public Scanner(Canvas canvas, Image image)
        {
            MainCanvas = canvas;
            MainCanvas.MouseMove += MainCanvas_MouseMove;
            MainCanvas.SizeChanged += MainCanvas_SizeChanged;
            
            DiagramImage = image;

            AxisX = new HorizontalAxis();
            AxisY = new VerticalAxis();
            MainCanvas.Children.Add(AxisX.LineObject);
            MainCanvas.Children.Add(AxisY.LineObject);
        }

        private void MainCanvas_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            AxisX.X2 = e.NewSize.Width;
            AxisY.Y2 = e.NewSize.Height;
        }

        private void MainCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if(IsAxisXMove)
                {
                    AxisX.SetY(e.GetPosition(MainCanvas).Y);
                }
                else if(IsAxisYMove)
                {
                    AxisY.SetX(e.GetPosition(MainCanvas).X);
                }
            }
        }

        public void OpenImage(string fileName)
        {
            ImageSource diagramImage = new BitmapImage(new Uri(fileName));
            MainCanvas.Width = diagramImage.Width;
            MainCanvas.Height = diagramImage.Height;
            DiagramImage.Source = diagramImage;
        }

        public void AxisXShow()
        {
            AxisX.Show();
        }

        public void AxisXHide()
        {
            AxisX.Hide();
        }

        public void AxisYShow()
        {
            AxisY.Show();
        }

        public void AxisYHide()
        {
            AxisY.Hide();
        }
    }
}

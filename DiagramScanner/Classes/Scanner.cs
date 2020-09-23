using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiagramScanner.Classes
{
    class Scanner
    {
        const double ScaleRate = 1.05;
        private ScaleTransform MainCanvasScale;
        private HorizontalAxis AxisX;
        private VerticalAxis AxisY;
        private VerticalAxis AxisXMax;
        private HorizontalAxis AxisYMax;
        private Axis UnderMouseObject;
        public Scanner DiagramScanner { get; set; }
        public Image DiagramImage { get; set; }
        public Canvas MainCanvas { get; set; }
        public bool IsAxisXMove { get; set; }
        public bool IsAxisYMove { get; set; }
        public bool IsAxisXMaxMove { get; set; }
        public bool IsAxisYMaxMove { get; set; }

        public Scanner(Canvas canvas, Image image)
        {
            MainCanvas = canvas;
            MainCanvas.MouseMove += MainCanvas_MouseMove;
            MainCanvas.SizeChanged += MainCanvas_SizeChanged;
            MainCanvas.MouseWheel += MainCanvas_MouseWheel;

            MainCanvasScale = new ScaleTransform();

            DiagramImage = image;

            AxisX = new HorizontalAxis(MainCanvas, Colors.Blue, 2, true);
            AxisX.MouseEnterEvent += AxisX_MouseEnterEvent;
            AxisY = new VerticalAxis(MainCanvas, Colors.Blue, 2, true);
            AxisY.MouseEnterEvent += AxisY_MouseEnterEvent;
            AxisXMax = new VerticalAxis(MainCanvas, Colors.DarkRed, 2, false);
            AxisXMax.MouseEnterEvent += AxisXMax_MouseEnterEvent;
            AxisYMax = new HorizontalAxis(MainCanvas, Colors.DarkRed, 2, false);
            AxisYMax.MouseEnterEvent += AxisYMax_MouseEnterEvent;
        }

        private void AxisYMax_MouseEnterEvent(object sender, EventArgs e)
        {
            UnderMouseObject = sender as HorizontalAxis;
        }

        private void AxisXMax_MouseEnterEvent(object sender, EventArgs e)
        {
            UnderMouseObject = sender as VerticalAxis;
        }

        private void AxisY_MouseEnterEvent(object sender, EventArgs e)
        {
            UnderMouseObject = sender as VerticalAxis;
        }

        private void AxisX_MouseEnterEvent(object sender, EventArgs e)
        {
            UnderMouseObject = sender as HorizontalAxis;
        }

        private void MainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                MainCanvas.LayoutTransform = MainCanvasScale;
                if (e.Delta > 0)
                {
                    MainCanvasScale.ScaleX *= ScaleRate;
                    MainCanvasScale.ScaleY *= ScaleRate;
                }
                else
                {
                    MainCanvasScale.ScaleX /= ScaleRate;
                    MainCanvasScale.ScaleY /= ScaleRate;
                }
                e.Handled = true;
            }            
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AxisX.X2 = e.NewSize.Width;
            AxisY.Y2 = e.NewSize.Height;
            AxisXMax.Y2 = e.NewSize.Height;
            AxisYMax.X2 = e.NewSize.Width;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && UnderMouseObject != null)
            {
                if (UnderMouseObject is HorizontalAxis horizontalAxis)
                {
                    horizontalAxis.SetY(e.GetPosition(MainCanvas).Y);
                }
                else if (UnderMouseObject is VerticalAxis verticalAxis)
                {
                    verticalAxis.SetX(e.GetPosition(MainCanvas).X);
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

        public void AxisXMaxShow()
        {
            AxisXMax.Show();
        }
        public void AxisXMaxHide()
        {
            AxisXMax.Hide();
        }
        public void AxisYMaxShow()
        {
            AxisYMax.Show();
        }
        public void AxisYMaxHide()
        {
            AxisYMax.Hide();
        }
    }
}

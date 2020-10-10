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
        private VerticalScaleMarker YScaleMarker;
        private HorizontalScaleMarker XScaleMarker;
        private Axis UnderMouseObject;
        public Scanner DiagramScanner { get; set; }
        public Image DiagramImage { get; set; }
        public Canvas MainCanvas { get; set; }
        public string XScale { get; set; }
        public string YScale { get; set; }

        public Scanner(Canvas canvas, Image image)
        {
            MainCanvas = canvas;
            MainCanvas.MouseMove += MainCanvas_MouseMove;
            MainCanvas.SizeChanged += MainCanvas_SizeChanged;
            MainCanvas.MouseWheel += MainCanvas_MouseWheel;

            MainCanvasScale = new ScaleTransform();

            DiagramImage = image;

            XScaleMarker = new HorizontalScaleMarker(MainCanvas, Colors.Blue, 2);
            XScaleMarker.MouseEnterEvent += XScaleMarker_MouseEnterEvent;
            YScaleMarker = new VerticalScaleMarker(MainCanvas, Colors.Blue, 2);
            YScaleMarker.MouseEnterEvent += YScaleMarker_MouseEnterEvent;

            AxisX = new HorizontalAxis(MainCanvas, Colors.Blue, 2, XScaleMarker);
            AxisX.MouseEnterEvent += AxisX_MouseEnterEvent;
            AxisY = new VerticalAxis(MainCanvas, Colors.Blue, 2, YScaleMarker);
            AxisY.MouseEnterEvent += AxisY_MouseEnterEvent;

            AxisXMax = new VerticalAxis(MainCanvas, Colors.DarkRed, 2);
            AxisXMax.MouseEnterEvent += AxisXMax_MouseEnterEvent;
            AxisYMax = new HorizontalAxis(MainCanvas, Colors.DarkRed, 2);
            AxisYMax.MouseEnterEvent += AxisYMax_MouseEnterEvent;
        }

        private void YScaleMarker_MouseEnterEvent(object sender, EventArgs e)
        {
            UnderMouseObject = sender as VerticalScaleMarker;
        }

        private void XScaleMarker_MouseEnterEvent(object sender, EventArgs e)
        {
            UnderMouseObject = sender as HorizontalScaleMarker;
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
                else if (UnderMouseObject is HorizontalScaleMarker horizontalScaleMarker)
                {
                    horizontalScaleMarker.SetX(e.GetPosition(MainCanvas).X);
                }
                else if (UnderMouseObject is VerticalScaleMarker verticalScaleMarker)
                {
                    verticalScaleMarker.SetY(e.GetPosition(MainCanvas).Y);
                }
            }
        }

        public void OpenImage(string fileName)
        {
            ImageSource diagramImage = new BitmapImage(new Uri(fileName));
            MainCanvas.Width = diagramImage.Width;
            MainCanvas.Height = diagramImage.Height;
            DiagramImage.Source = diagramImage;
            AxisX.SetY(MainCanvas.Height / 2);
            AxisY.SetX(MainCanvas.Width / 2);
            AxisXMax.SetX(MainCanvas.Width / 2);
            AxisYMax.SetY(MainCanvas.Height / 2);
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

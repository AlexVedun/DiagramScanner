using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        private double xScale;
        private double yScale;
        private ObservableCollection<Point> PrimaryCollection;
        private ObservableCollection<Point> CalculatedCollection;
        public Scanner DiagramScanner { get; set; }
        public Image DiagramImage { get; set; }
        public Canvas MainCanvas { get; set; }
        public string XUnit { get; set; }
        public string YUnit { get; set; }       
        public double XScale
        {
            get { return xScale; }
        }
        public double YScale
        {
            get { return yScale; }            
        }

        public event EventHandler ScaleCalculatedEvent;

        public Scanner(Canvas canvas, Image image, DataGrid primaryDataGrid, DataGrid calculatedDataGrid)
        {
            MainCanvas = canvas;
            MainCanvas.MouseMove += MainCanvas_MouseMove;
            MainCanvas.SizeChanged += MainCanvas_SizeChanged;
            MainCanvas.MouseWheel += MainCanvas_MouseWheel;
            MainCanvas.MouseRightButtonDown += MainCanvas_MouseRightButtonDown;

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

            PrimaryCollection = new ObservableCollection<Point>();
            primaryDataGrid.ItemsSource = PrimaryCollection;
            CalculatedCollection = new ObservableCollection<Point>();
            calculatedDataGrid.ItemsSource = CalculatedCollection;
        }

        private void MainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            double pointX = e.GetPosition(MainCanvas).X;
            double pointY = e.GetPosition(MainCanvas).Y;
            double x = pointX - AxisY.X1;
            double y = AxisX.Y1 - pointY;
            Point point = new Point(x * XScale, y * YScale);

            PrimaryCollection.Add(point);
            for (int i = 0; i < PrimaryCollection.Count - 1; i++)
            {
                Point pI = PrimaryCollection[i];
                for (int j = i + 1; j < PrimaryCollection.Count; j++)
                {
                    Point pJ = PrimaryCollection[j];
                    if (pI.X > pJ.X)
                    {
                        PrimaryCollection.Move(j, i);
                    }
                }
            }

            Ellipse ellipse = new Ellipse();
            ellipse.Width = 3;
            ellipse.Height = 3;
            ellipse.StrokeThickness = 1;             
            ellipse.Fill = new SolidColorBrush(Colors.Red);
            MainCanvas.Children.Add(ellipse);
            Canvas.SetTop(ellipse, pointY - 1.5);
            Canvas.SetLeft(ellipse, pointX - 1.5);
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
            XScaleMarker.SetX(MainCanvas.Width / 2);
            YScaleMarker.SetY(MainCanvas.Height / 2);
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

        public void CalculateScale()
        {
            if (XUnit != "")
            {
                double xPixels = XScaleMarker.X1 - AxisY.X1;
                xScale = Globals.GetDouble(XUnit, 0) / xPixels;
            }
            if (YUnit != "")
            {
                double yPixels = AxisX.Y1 - YScaleMarker.Y1;
                yScale = Globals.GetDouble(YUnit, 0) / yPixels;
            }
            ScaleCalculatedEvent?.Invoke(this, EventArgs.Empty);    
        }
    }
}

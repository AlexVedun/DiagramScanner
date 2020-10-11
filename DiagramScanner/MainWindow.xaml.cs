using DiagramScanner.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiagramScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Scanner scanner;
        public MainWindow()
        {
            InitializeComponent();

            scanner = new Scanner(MainCanvas, MainImage, PrimaryDataGrid, CalculatedDataGrid);
            scanner.ScaleCalculatedEvent += Scanner_ScaleCalculatedEvent;
        }

        private void Scanner_ScaleCalculatedEvent(object sender, EventArgs e)
        {
            XScaleLabel.Content = scanner.XScale.ToString("F3") + " ед/пикс.";
            YScaleLabel.Content = scanner.YScale.ToString("F3") + " ед/пикс.";
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.bmp)|*.png;*.jpeg;*bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                scanner.OpenImage(openFileDialog.FileName);
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();
            points.Add(new Point(0, 0));
            points.Add(new Point(5, 10));
            points.Add(new Point(20, 8));
            points.Add(new Point(35, 20));
            points.Add(new Point(40, 40));
            points.Add(new Point(100, 100));
            polyline.Points = points;
            polyline.Stroke = new SolidColorBrush(Colors.Blue);
            MainCanvas.Children.Add(polyline);
        }

        private void AxisXShowCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            scanner.AxisXShow();
        }

        private void AxisXShowCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            scanner.AxisXHide();
        }

        private void AxisYShowCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            scanner.AxisYShow();
        }

        private void AxisYShowCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            scanner.AxisYHide();
        }

        private void AxisXMaxShowCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            scanner.AxisXMaxShow();
        }

        private void AxisXMaxShowCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            scanner.AxisXMaxHide();
        }

        private void AxisYMaxShowCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            scanner.AxisYMaxShow();
        }

        private void AxisYMaxShowCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            scanner.AxisYMaxHide();
        }

        private void XUnitTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            scanner.XUnit = textBox.Text;
            scanner.CalculateScale();
        }

        private void YUnitTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            scanner.YUnit = textBox.Text;
            scanner.CalculateScale();
        }
    }
}

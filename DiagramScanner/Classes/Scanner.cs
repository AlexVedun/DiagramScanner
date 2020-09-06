using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DiagramScanner.Classes
{
    class Scanner
    {
        public Scanner DiagramScanner { get; set; }
        public Image DiagramImage { get; set; }
        public Canvas MainCanvas { get; set; }
        public void OpenImage(string fileName)
        {
            ImageSource diagramImage = new BitmapImage(new Uri(fileName));
            MainCanvas.Width = diagramImage.Width;
            MainCanvas.Height = diagramImage.Height;
            DiagramImage.Source = diagramImage;
        }
    }
}

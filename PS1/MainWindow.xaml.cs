using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PS1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WriteableBitmap EditableImage { get; set; }
        public double CurrentImageScale { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"+ 
                "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";

            if (openFileDialog.ShowDialog() == true)
            {
                EditableImage = new WriteableBitmap(new BitmapImage(new Uri(openFileDialog.FileName)));
            }

            Image.Source = EditableImage;
        }

        private void PlusSize(object sender, RoutedEventArgs e)
        {
            var scale = (ScaleTransform)((TransformGroup)Image.RenderTransform).Children.FirstOrDefault(tr => tr is ScaleTransform);
            if (scale.ScaleX > 0 && scale.ScaleY > 0)
            {
                scale.ScaleX += 0.2;
                scale.ScaleY += 0.2;
            }
        }

        private void MinusSize(object sender, RoutedEventArgs e)
        {
            var scale = (ScaleTransform)((TransformGroup)Image.RenderTransform).Children.FirstOrDefault(tr => tr is ScaleTransform);
            if (scale.ScaleX - 0.2 > 0 && scale.ScaleY - 0.2 > 0)
            {
                scale.ScaleX -= 0.2;
                scale.ScaleY -= 0.2;
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = "Document";
            dlg.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|" +
                "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            if (dlg.ShowDialog() == true)
            {
                var encoder = new JpegBitmapEncoder(); // Or PngBitmapEncoder, or whichever encoder you want
                encoder.Frames.Add(BitmapFrame.Create(EditableImage));
                using (var stream = dlg.OpenFile())
                {
                    encoder.Save(stream);
                }
            }
        }
    }
}

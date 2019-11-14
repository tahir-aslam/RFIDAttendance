using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace BarcodeAttendanceSystem_WPF_.Converter
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is byte[]))
                return null;

            BitmapImage image = new BitmapImage();
            MemoryStream ms = new MemoryStream((byte[])value);

            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

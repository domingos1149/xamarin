﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Modulo1.Converters
{
    public class ByteToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource objImageSource;

            if (value != null)
            {
                byte[] byteImageData = (byte[]) value; 
                objImageSource = ImageSource.FromStream(() => new MemoryStream(byteImageData));
            }
            else
            {
                objImageSource = null;
            }
                                
            return objImageSource;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
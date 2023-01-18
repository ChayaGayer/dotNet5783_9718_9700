using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using BO;
using System.Windows.Media;
using System.Windows;

namespace PL
{
    class ConvertImagePathToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            try
            {
                string imageRelativeName = (string)value;
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + imageRelativeName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
            catch (Exception ex)
            {
                string imageRelativeName = @"\Pics\IMG_FAILURE.png";
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + imageRelativeName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;

            }
        }
       

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    class ConvertColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OrderStatus status = (OrderStatus)value;
            if (status == OrderStatus.Ordered) { return Brushes.Red; }
            if (status == OrderStatus.Shipped) { return Brushes.Orange; }
            else
            { return Brushes.Green; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    

    class ConvertTextToBoolean : IMultiValueConverter //As long as one of the product details is empty, it will not be possible to update or add a new product
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {

            while (value[0].ToString() == "" || value[1].ToString() == "" || value[2].ToString() == "" || value[3].ToString() == "")
            {
                return false;
            }
            return true;

        }
        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
    class ConvertTimeToProgressBar : IValueConverter
    {
      private BlApi.IBl bl = BlApi.Factory.Get();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime timSim;
            //if (timSim == null)
                timSim = DateTime.Now;
            BO.Order order = new();
            int id = (int)value;
            try
            {
                order = bl.Order.RequestOrderDeta(id);
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlAlreadyExistEntityException  ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (order.Status == BO.OrderStatus.Delivered)
                return 100;
            if (order.Status == BO.OrderStatus.Ordered)
                return 30;
            else
                return 70;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    
}







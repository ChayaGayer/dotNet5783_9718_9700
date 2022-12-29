using BO;
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
using PL.Product;
using PL.Orders;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ProductList(object sender, RoutedEventArgs e) => new Window1().Show();

      

        private void Track_Click(object sender, RoutedEventArgs e)
        {
            new OrderTrack().ShowDialog();
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            new ProductItemWindow().Show();
        }



















        //new ProductListWindow(bl).Show();



    }
}

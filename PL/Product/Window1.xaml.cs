using PL.Orders;
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
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        static readonly BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        /// a window constructor
        /// </summary>
        public Window1()
        {
            InitializeComponent();
        }

     

      /// <summary>
      /// open the productList
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>

        private void product_click(object sender, RoutedEventArgs e)
        {

            new ProductListWindow(bl).Show();
        }
        /// <summary>
        /// open the ordersList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orders_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(bl).Show();
        }
    }
}

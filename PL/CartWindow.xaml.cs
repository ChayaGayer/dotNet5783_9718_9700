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

namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public BO.Cart? CartPl
        {
            get { return (BO.Cart?)GetValue(CartPlProperty); }
            set { SetValue(CartPlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CartPl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartPlProperty =
            DependencyProperty.Register("CartPl", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));



        
        public CartWindow()
        {
            InitializeComponent();
            Cart.Visibility = Visibility.Hidden;
            btnConfirm.Visibility = Visibility.Hidden;
        }


        
        private void FinishOrder_Click(object sender, RoutedEventArgs e)
        {
            Cart.Visibility = Visibility.Visible;
            productItemListView.Visibility = Visibility.Hidden;
            btnFinishOrder.Visibility = Visibility.Visible;
            btnConfirm.Visibility = Visibility.Visible;
            btnFinishOrder.Visibility = Visibility.Hidden;
        }

        private void Confirm_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //btnFinishOrder.Visibility = Visibility.Visible;
            MessageBox.Show("Are you sure you want to complite the order?");
        }

        
    }
}

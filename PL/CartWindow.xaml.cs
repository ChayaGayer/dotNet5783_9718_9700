using BlApi;
using BO;
using PL.Product;
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
        BO.Cart cart2;
        public BO.Cart? CartPl
        {
            get { return (BO.Cart?)GetValue(CartPlProperty); }
            set { SetValue(CartPlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CartPl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartPlProperty =
            DependencyProperty.Register("CartPl", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));



        
        public CartWindow(BO.Cart? MyCart)
        {
            CartPl = MyCart;
            InitializeComponent();
            orderItemListView.ItemsSource = CartPl.Items;
            orderItemListView.Items.Refresh();
            CartGrid.Visibility = Visibility.Hidden;
            btnConfirmOrder.Visibility = Visibility.Hidden;


        }



        private void FinishOrder_Click(object sender, RoutedEventArgs e)
        {
            CartGrid.Visibility = Visibility.Visible;
            orderItemListView.Visibility = Visibility.Hidden;
            orderItemListView.Visibility = Visibility.Visible;
            orderItemListView.Visibility = Visibility.Visible;
            orderItemListView.Visibility = Visibility.Hidden;
        }


        private void AddOne_Click(object sender, RoutedEventArgs  e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
            try
            {
                CartPl = bl.Cart.UpdateAmountOfProduct(CartPl, orderItem.ItemId, orderItem.Amount + 1);
            }
            catch (BO.BlInCorrectException ex)
            {
                MessageBox.Show(ex.Message, "Try again", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (BO.BlNagtiveNumberException ex)
            {
                MessageBox.Show(ex.Message, "Try again", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, "Try again", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            orderItemListView.ItemsSource = CartPl.Items;
            orderItemListView.Items.Refresh();
            TotalPriceTB.Text = CartPl.TotalPrice.ToString();


        }

        private void Minus_Click(object sender, RoutedEventArgs  e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
            try
            {
                CartPl = bl.Cart.UpdateAmountOfProduct(CartPl, orderItem.ItemId, orderItem.Amount - 1);
            }
            catch(BO.BlInCorrectException ex)
            {
                 MessageBox.Show(ex.Message,"Try again",MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (BO.BlNagtiveNumberException ex)
            {
                MessageBox.Show(ex.Message, "Try again", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, "Try again", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
           
            //orderItemListView.Items.Refresh();
            orderItemListView.ItemsSource = CartPl.Items;
            orderItemListView.Items.Refresh();
            TotalPriceTB.Text = CartPl.TotalPrice.ToString();


        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
            try
            {
                CartPl = bl.Cart.UpdateAmountOfProduct(CartPl, orderItem.ItemId, 0);
            }
            catch (BO.BlInCorrectException ex)
            {
                MessageBox.Show(ex.Message, "Try again", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (BO.BlNagtiveNumberException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //orderItemListView.Items.Refresh();
            orderItemListView.ItemsSource = CartPl.Items;
            orderItemListView.Items.Refresh();
            TotalPriceTB.Text = CartPl.TotalPrice.ToString();


        }

        private void btnConfirmOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnFinishOrder.Visibility = Visibility.Hidden;
            MessageBox.Show("Are you sure you want to complite the order?");

            try
            {
                bl.Cart.OrderConfirmation(CartPl);
                MessageBox.Show("Your order has been accepted,Thank you buing at swarovski" +
                    "🛍 ");
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlNagtiveNumberException ex)
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlEmptyStringException ex)
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnFinishOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CartGrid.Visibility = Visibility.Visible;
            orderItemListView.Visibility = Visibility.Hidden;
            orderItemListView.Visibility = Visibility.Visible;
            orderItemListView.Visibility = Visibility.Visible;
            orderItemListView.Visibility = Visibility.Hidden;
            btnFinishOrder.Visibility = Visibility.Hidden;
            btnConfirmOrder.Visibility = Visibility.Visible;


        }
    }
}

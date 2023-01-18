
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
using BlApi;
using BO;
using System.Collections.Generic;
using System;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        /// a window constructor
        /// </summary>
        public MainWindow()
        {   
            InitializeComponent();
            NewOrder.Visibility = Visibility.Hidden;
            TrackBtn.Visibility = Visibility.Hidden;
        }
        BO.Cart CurrentCart;
        /// <summary>
        /// constructor with parameter
        /// </summary>
        /// <param name="Mycart"></param>
        public MainWindow(BO.Cart Mycart)
        {
            CurrentCart = Mycart;//the cart 
            InitializeComponent();
            simulatorB.Visibility = Visibility.Hidden;//for customer hide the simulator button
            MangerBtn.Visibility = Visibility.Hidden;//for customer hide the maneger button
            CurrentCart.CustomerAdress = " ";
            CurrentCart.Items = new List<BO.OrderItem?>();
            CurrentCart.TotalPrice = 0;

        }
        //
        /// <summary>
        /// open window for maneger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductList(object sender, RoutedEventArgs e) => new Window1().Show();

        /// <summary>
        /// for tracking an order -open text box to fill the id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Track_Click(object sender, RoutedEventArgs e)
        {
            OrderId.Visibility = Visibility.Visible;
            enterId.Visibility = Visibility.Visible;
            Check.Visibility = Visibility.Visible;
            // new OrderTrack().ShowDialog();
        }
        /// <summary>
        /// open the Catalog for the custumer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NewOrder_Click(object sender, RoutedEventArgs e)//private
        {

            new ProductItemWindow(CurrentCart).ShowDialog();
        }
        /// <summary>
        /// a button that check the id and if exust open the correct order tracking 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(OrderId.Text);
            try
            {
                new OrderTrack(id).Show();
            }
            catch (BlMissingEntityException ex)
            {
                {
                    MessageBox.Show(ex.Message, "The id not exist", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }/// <summary>
         /// PreviewKeyDown allow kees and digits for the id 
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void OrderId_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
            || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down ||
            e.Key == Key.Right || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other

            return;
        }
        /// <summary>
        /// open the simulator window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            new Simulator().Show();

        }
        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }


}


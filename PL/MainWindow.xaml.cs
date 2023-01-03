﻿using BO;
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
            OrderId.Visibility = Visibility.Visible;
            enterId.Visibility = Visibility.Visible;
            Check.Visibility = Visibility.Visible;
           // new OrderTrack().ShowDialog();
        }

        public void NewOrder_Click(object sender, RoutedEventArgs e)//private
        {
            
            //BO.Cart MyCart = new BO.Cart();
            BO.Cart MyCart = new BO.Cart
            {
                CustomerName = " ",
                CustomerAdress = " ",
                CustomerEmail = " ",
                Items = new List<BO.OrderItem?>(),
                TotalPrice = 0
            };


            new ProductItemWindow(MyCart).Show();
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(OrderId.Text);
            
            new OrderTrack(id).Show();
        }

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
            e.Key == Key.Right||e.Key==Key.NumPad0||e.Key==Key.NumPad1|| e.Key == Key.NumPad2|| e.Key == Key.NumPad3|| e.Key == Key.NumPad4|| e.Key == Key.NumPad5|| e.Key == Key.NumPad6|| e.Key == Key.NumPad7|| e.Key == Key.NumPad8|| e.Key == Key.NumPad9)
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
    }



    }


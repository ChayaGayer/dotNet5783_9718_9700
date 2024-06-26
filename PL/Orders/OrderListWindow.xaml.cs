﻿using BlApi;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        ///  a window condtructor according the xaml
        /// </summary>
        /// <param name="bl"></param>
        public OrderListWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            orderForListDataGrid.ItemsSource = bl.Order.GetListedOrders();
        }
        

        /// <summary>
        /// open the selected order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderForListDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? OrderL = orderForListDataGrid.SelectedItem as BO.OrderForList;
            if(OrderL != null)
            {
                OrderWindow orderWindow = new OrderWindow(OrderL.ID);
                orderWindow.ShowDialog();
                orderForListDataGrid.ItemsSource=bl.Order.GetListedOrders();
            }
            
        }
    }
}

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
using BO;
using BlApi;
using BlImplementation;
using PL.Product;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = new Bl();
        public MainWindow()
        {
            InitializeComponent();
        }
       
       
        //private void ShowProuductListButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow(bl).Show();

        private void ProductList(object sender, RoutedEventArgs e) => new ProductListWindow(bl).Show();
        

        
    }
}
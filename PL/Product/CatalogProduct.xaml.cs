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
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for CatalogProduct.xaml
    /// </summary>
    public partial class CatalogProduct : Window
    {


        BlApi.IBl bl = BlApi.Factory.Get();
        BO.Cart cart;


       
        public BO.Product? CatalogProductPL
        {
            get { return (BO.Product?)GetValue(CatalogProductPLProperty); }
            set { SetValue(CatalogProductPLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CatalogProductPL.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CatalogProductPLProperty =
            DependencyProperty.Register("CatalogProductPL", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));


        public CatalogProduct()
        {
            InitializeComponent();
        }
        public CatalogProduct(BO.Cart MyCart , int id)
        {
            cart= MyCart;   
            InitializeComponent();
            try
            {
                CatalogProductPL = bl.Product.RequestProductDetaForM(id);
            }
            catch( BO.BlInCorrectException ex)
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            int textId = int.Parse(iDTextBlock.Text);
            try
            {

                cart = bl.Cart.AddProductForCart(cart, textId);
                MessageBox.Show("The product added sucssesfully");
            }
            catch(BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
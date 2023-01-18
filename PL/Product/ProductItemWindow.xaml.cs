using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        
        public ObservableCollection<BO.ProductItem?> ListProductItems
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(ListProductItemsProperty); }
            set { SetValue(ListProductItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListProductItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListProductItemsProperty =
            DependencyProperty.Register("ListProductItems", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));


        BO.Cart cart1;

        /// <summary>
        /// a window condtructor according the xaml,no parameters
        /// </summary>
        public ProductItemWindow()
        {
            InitializeComponent();
            ListViewProductItems.ItemsSource = bl.Product.GetListedProductsForC();//bring the catalog from the bl
        }
        /// <summary>
        /// a window condtructor according the xaml,with cart
        /// </summary>
        /// <param name="cart"></param>
        public ProductItemWindow(BO.Cart cart)
        {
            InitializeComponent();
            ListViewProductItems.ItemsSource = bl.Product.GetListedProductsForC();
            cart1 = cart;
        }
        /// <summary>
        /// open the cart window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart1).ShowDialog();
        }
        /// <summary>
        /// double click to see the productItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem? productItem= ListViewProductItems.SelectedItem as BO.ProductItem;//the select product
           if(productItem!=null)
            {
                CatalogProduct catalogProduct = new CatalogProduct(cart1, productItem.ID);
                catalogProduct.ShowDialog();//open this product window
            }
        }

        private void BracletME_MouseEnter(object sender, MouseEventArgs e)
        {
             ListViewProductItems.ItemsSource = bl.Product.GetListedProducts(p => p?.Category == Category.Braclet);
          //  ListProductItems =  new ObservableCollection<ProductItem?>(bl.Product.GetListedProducts(p => (BO.Category)p?.Category == Category.Braclet);
        }

        private void RefrashME_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewProductItems.ItemsSource = bl.Product.GetListedProducts();
        }

        private void EarringsMe_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewProductItems.ItemsSource = bl.Product.GetListedProducts(p => p?.Category == Category.Earrings);
        }

        private void RingMe_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewProductItems.ItemsSource = bl.Product.GetListedProducts(p => p?.Category == Category.Ring);
        }

        private void NecklessMe_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewProductItems.ItemsSource = bl.Product.GetListedProducts(p => p?.Category == Category.Neckless);
        }

        private void WatchME_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewProductItems.ItemsSource = bl.Product.GetListedProducts(p => p?.Category == Category.Watch);
        }

        private void BestSelerME_MouseEnter(object sender, MouseEventArgs e)
        {

            ListViewProductItems.ItemsSource = bl.Product.MostPopular();
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();   
        }
    }
}

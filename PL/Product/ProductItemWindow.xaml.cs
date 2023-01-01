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


        public ProductItemWindow()
        {
            InitializeComponent();
            productItemListView.ItemsSource = bl.Product.GetListedProductsForC();
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow().Show();
        }
    }
}

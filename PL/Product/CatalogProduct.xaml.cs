using BO;
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


       
        public BO.ProductItem? CatalogProductPL
        {
            get { return (BO.ProductItem?)GetValue(CatalogProductPLProperty); }
            set { SetValue(CatalogProductPLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CatalogProductPL.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CatalogProductPLProperty =
            DependencyProperty.Register("CatalogProductPL", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        /// A constructor for the window according the xaml
        /// </summary>
        public CatalogProduct()
        {
            InitializeComponent();
        }
        /// <summary>
        /// A constructor for the window according the xaml with cart and id
        /// </summary>
        /// <param name="MyCart"></param>
        /// <param name="id"></param>
        public CatalogProduct(BO.Cart MyCart , int id)
        {
            cart= MyCart;   
            InitializeComponent();
            try
            {
                CatalogProductPL =bl.Product.RequestProductDetaForC(id,cart);
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
        /// <summary>
        /// add the item for the cart by id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            this.Close();
        }
    }
}

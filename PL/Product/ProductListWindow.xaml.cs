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
using BlApi;
using BO;


using DO;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
  
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        /// <summary>
        /// constructor for the product list window 
        /// </summary>
        /// <param name="bl"></param>
        public ProductListWindow(IBl? bl)
        {
            InitializeComponent();
            BO.Category category = new BO.Category();   
            Category.ItemsSource=Enum.GetValues(typeof(BO.Category));
            ProductListView.ItemsSource = bl?.Product.GetListedProducts();
            //Category.Items.Add(BO.Category.Braclet);
            //Category.Items.Add(BO.Category.Earrings);
            //Category.Items.Add(BO.Category.Neckless);
            //Category.Items.Add(BO.Category.Ring);
            //Category.Items.Add(BO.Category.Watch);
            //Category.Items.Add("None");

        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            BO.Category category = (BO.Category)Category.SelectedItem;
            if (category == BO.Category.None)
            {
                ProductListView.ItemsSource = bl?.Product.GetListedProducts();
            }
            else {
                ProductListView.ItemsSource = bl?.Product.GetListedProducts(x => x?.Category == category);
            }
        }
        
        
        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

    
        /// <summary>
        /// ADD a new Product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();//open the product window
            ProductListView.ItemsSource = bl?.Product.GetListedProducts();//show the update list
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int id = ((ProductForList)ProductListView.SelectedItem).ID;//get the id
            new ProductWindow(id).ShowDialog();//open the product window with the update button
            ProductListView.ItemsSource = bl?.Product.GetListedProducts();//show the update list
        }
    }
}

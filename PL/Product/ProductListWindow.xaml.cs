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
using BlApi;
using BO;


namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
  
    public partial class ProductListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

        /// <summary>
        /// constructor for the product list window 
        /// </summary>
        /// <param name="bl"></param>
        public ProductListWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            BO.Category category = new BO.Category();   
            Category.ItemsSource=Enum.GetValues(typeof(BO.Category));
            prodList = new List<BO.ProductForList?>(bl.Product.GetListedProducts());



        }

        /// <summary>
        /// Dependency Property 
        /// </summary>
        public List<BO.ProductForList?>  prodList
        {
            get { return (List<BO.ProductForList?>) GetValue(prodListProperty); }
            set { SetValue(prodListProperty, value); }
        }
       

        // Using a DependencyProperty as the backing store for prodList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty prodListProperty =
            DependencyProperty.Register("prodList", typeof(List<BO.ProductForList?>), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        /// Displays the list by selected catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            BO.Category? category = Category.SelectedItem as BO.Category?;
            if (category == BO.Category.None)
            {
                //productForListDataGrid.ItemsSource = bl.Product.GetListedProducts();
                prodList = new List<BO.ProductForList?> (bl.Product.GetListedProducts());
            }
            else {
                //productForListDataGrid.ItemsSource = bl.Product.GetListedProducts(x => x?.Category == category);
                prodList = new List<BO.ProductForList?>(bl.Product.GetListedProducts(x => x?.Category == category));

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
           // ProductListView.ItemsSource = bl.Product.GetListedProducts();//show the update list
        }
        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            //BO.ProductForList? prodList = productForListDataGrid.SelectedItems as BO.ProductForList;
            int id = ((ProductForList)productForListDataGrid.SelectedItem).ID;//get the id-
             new ProductWindow(id).ShowDialog();//open the product window with the update button
                                                //productForListDataGrid.ItemsSource = bl.Product.GetListedProducts();//show the update list-איך רואין ריענון
            //prodList = new List<BO.ProductForList?>(bl.Product.GetListedProducts());

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            prodList = new List<BO.ProductForList?>(bl.Product.GetListedProducts());

        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}

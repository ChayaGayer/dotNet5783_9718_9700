
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
using BO;
using System.Diagnostics;
using System.ComponentModel;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    /// 
    public partial class ProductWindow : Window
    {
         static readonly BlApi.IBl bl = BlApi.Factory.Get();

        /// <summary>
        /// constructor for the add
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            selection.ItemsSource = Enum.GetValues(typeof(BO.Category));
            UpdateProduct.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Visible;
            Add.Content = "Add";

        }


        public BO.Product? productPl
        {
            get { return (BO.Product?)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }

        // Using a DependencyProperty as the backing store for product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productProperty =
            DependencyProperty.Register("productPl", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));


        /// <summary>
        /// constructor for the update
        /// </summary>
        /// <param name="id"></param>
        public ProductWindow(int id)
        {
            InitializeComponent();
            //product=
            selection.ItemsSource = Enum.GetValues(typeof(BO.Category));
            BO.Product product = bl!.Product.RequestProductDetaForM(id);
            IDtextBox.Text = product?.ID.ToString();
            IDtextBox.IsReadOnly = true;
            IDtextBox.Foreground = Brushes.Red;
            selection.Text = product?.Category.ToString();
            Name.Text = product?.ProductName;
            Price.Text = product?.Price.ToString();
            Amount.Text = product?.InStock.ToString();
            UpdateProduct.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Collapsed;//close the add button
            UpdateProduct.Content = "Update";


        }
        private void selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
       
        /// <summary>
        /// adding a product to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;

            try
            {
                BO.Product product = new BO.Product()
                 {

                ID = int.Parse(IDtextBox.Text),
                Price = int.Parse(Price.Text),
                ProductName = Name.Text,
                InStock = int.Parse(Amount.Text),
                Category = (BO.Category)selection.SelectedItem
                };
                bl.Product.AddProduct(product);
                messageBoxResult = MessageBox.Show("Product Add succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
            } 
   
           catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.Message);


            }
            catch (BO.BlAlreadyExistEntityException ex)
            {
                MessageBox.Show(ex.Message,"ERROR", MessageBoxButton.OK,MessageBoxImage.Error);


            }
            catch (BO.BlEmptyStringException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            catch (BO.BlInCorrectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (BO.BlNagtiveNumberException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (BO.BlWorngCategoryException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (BO.BlIncorrectDatesException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void SelectCategory(object sender, SelectionChangedEventArgs e)
        {
            selection.ItemsSource = Enum.GetValues(typeof(BO.Category));

        }

        /// <summary>
        /// update the product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProduct_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                BO.Product product = new BO.Product();
                product.ID = int.Parse(IDtextBox.Text);
                product.ProductName = Name.Text;
                product.InStock = int.Parse(Amount.Text);
                product.Price = int.Parse(Price.Text);
                product.Category = (BO.Category)selection.SelectedItem;
                bl?.Product.UpdateProductData(product);
                messageBoxResult = MessageBox.Show("Product update succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            catch (BO.BlAlreadyExistEntityException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            catch (BO.BlEmptyStringException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            catch (BO.BlInCorrectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (BO.BlNagtiveNumberException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (BO.BlWorngCategoryException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (BO.BlIncorrectDatesException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

      
       

        

        

        
       

        

      

     

        private void Price_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys

            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)

            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;


            e.Handled = true;

            return;
        }

        private void IDtextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys

            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)

            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;


            e.Handled = true;

            return;
        }

        private void Amount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys

            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)

            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;


            e.Handled = true;

            return;
        }
    }
}

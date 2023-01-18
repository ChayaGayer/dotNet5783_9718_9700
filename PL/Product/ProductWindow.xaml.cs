
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
        public ProductWindow()//Window Constructor,not get parameters
        {
             InitializeComponent();
             categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
             UpdateProduct.Visibility = Visibility.Collapsed;//close the update button
             AddProduct.Visibility = Visibility.Visible;//visible the add one
            AddProduct.Content = "Add";
            try
            {
                productPl = new BO.Product();//new product
            }
            catch(BO.BlMissingEntityException ex)
            { productPl = null;//if couldnt throw exception
                MessageBox.Show(ex.Message," Operation Fail",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                this.Close();
            }
        }
        /// <summary>
        /// Dependency Property for product
        /// </summary>

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
            
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            UpdateProduct.Visibility = Visibility.Visible;
            AddProduct.Visibility = Visibility.Collapsed;
            productPl=bl.Product.RequestProductDetaForM(id);
           
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
            

            try
            {
               
                bl?.Product.AddProduct(productPl!);//adding the product that now in the dp
                MessageBox.Show($"Product successfully added!", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (BlAlreadyExistEntityException ex) { MessageBox.Show(ex.Message, " Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation); }



            catch (BO.BlNullPropertyException ex)
            {

                MessageBox.Show(ex.Message);


            }
            catch(BlEmptyStringException ex) { MessageBox.Show(ex.Message); } 
            catch(BlNagtiveNumberException ex) { MessageBox.Show(ex.Message); }
        }
        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Product.UpdateProductData(productPl!);
                MessageBox.Show($"Product successfully updated!", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (BlMissingEntityException ex)
            {
                {
                    MessageBox.Show(ex.Message, " Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

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
        /// <summary>
        /// PreviewKeyDown allow kees and digits for the id  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
            e.Key == Key.Right || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
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
        /// <summary>
        /// PreviewKeyDown allow kees and digits for the price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void priceTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
            e.Key == Key.Right || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
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
        /// <summary>
        /// PreviewKeyDown allow kees and digits for in stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inStockTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
            e.Key == Key.Right || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
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

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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public BO.Order? OrderPl
        {
            get { return (BO.Order?)GetValue(OrderPlProperty); }
            set { SetValue(OrderPlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderPl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderPlProperty =
            DependencyProperty.Register("OrderPl", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        ///  a window condtructor according the xaml
        /// </summary>
        public OrderWindow()
        {
            
            InitializeComponent();
        }
        public OrderWindow(int id)
        {
            
            InitializeComponent();
            
            try
            {
                OrderPl = bl.Order.RequestOrderDeta(id);
            }
            catch(BO.BlInCorrectException ex)
            {
                OrderPl=null;   
                MessageBox.Show(ex.Message," Invalied id",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }
        /// <summary>
        /// update the dates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bl.Order.UpdateSupplyOrder(OrderPl!.ID);
                OrderPl = bl.Order.RequestOrderDeta(OrderPl.ID);
            }
            catch(BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BO.BlIncorrectDatesException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
        /// <summary>
        /// update the dates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bl.Order.UpdateSendOrder(OrderPl!.ID);
                OrderPl = bl.Order.RequestOrderDeta(OrderPl.ID);
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlIncorrectDatesException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

       
    }
}

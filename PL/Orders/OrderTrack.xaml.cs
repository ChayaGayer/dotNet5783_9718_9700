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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderTrack.xaml
    /// </summary>
    public partial class OrderTrack : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public OrderTrack()
        {
            InitializeComponent();
        }
        public OrderTrack(int id)
        {
            InitializeComponent();
            try
            {
                OrderPl = bl.Order.OrderTracking(id);
               
            }
            catch (BO.BlInCorrectException ex)
            {
                OrderPl = null;
                MessageBox.Show(ex.Message, " Invalied id", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public BO.OrderTracking? OrderPl
        {
            get { return (BO.OrderTracking?)GetValue(OrderPlProperty); }
            set { SetValue(OrderPlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderPl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderPlProperty =
            DependencyProperty.Register("OrderPl", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


      

   
       
    }
    }


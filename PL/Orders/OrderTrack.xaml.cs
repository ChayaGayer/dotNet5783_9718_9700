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

        public BO.Order? OrderPl
        {
            get { return (BO.Order?)GetValue(OrderPlProperty); }
            set { SetValue(OrderPlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderPl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderPlProperty =
            DependencyProperty.Register("OrderPl", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));


        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
            || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
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

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(TextBoxId.Text);
            OrderPl = bl.Order.RequestOrderDeta(id);
            new OrderWindow(OrderPl.ID).Show();   
        }

       
    }
    }


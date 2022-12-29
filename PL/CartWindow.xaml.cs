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

namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
        }


        public BO.Cart CartPl
        {
            get { return (BO.Cart)GetValue(CartPlProperty); }
            set { SetValue(CartPlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CartPl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartPlProperty =
            DependencyProperty.Register("CartPl", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


    }
}

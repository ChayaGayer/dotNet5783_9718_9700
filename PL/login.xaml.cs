using BO;
using PL.Orders;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

        public BO.User MyUser
        {
            get { return (BO.User)GetValue(MyUserProperty); }
            set { SetValue(MyUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyUserProperty =
            DependencyProperty.Register("MyUser", typeof(BO.User), typeof(Window), new PropertyMetadata(null));




       // public BO.Cart myCart;
        public string myLogOrSign;
        public int myStatus;
        BO.Cart myCart = new BO.Cart
        {
            CustomerName = " ",
            CustomerAdress = " ",
            CustomerEmail = " ",
            Items = new List<BO.OrderItem?>(),
            TotalPrice = 0
        };
        public login()
        {
          
            InitializeComponent();
            //myLogOrSign = logOrSign;
            //MyUser = new BO.User();
            //myStatus = status;
        }



        private void btnLogIn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (myLogOrSign == "log")
                    MyUser = bl.User.GetByUserPasswprd(MyUser.Email!, MyUser.Password!)!;
                else
                {
                    bl?.User.AddUser(MyUser, myStatus);
                    MyUser.LogIn = (BO.UserLogIn)myStatus;
                }

                myCart.CustomerName = MyUser.Name;
                myCart.CustomerEmail = MyUser.Email;
                this.Close();
                if (MyUser.LogIn == BO.UserLogIn.Coustomer)
                {
                    new MainWindow(myCart).ShowDialog();
                }
                if (MyUser.LogIn == BO.UserLogIn.Maneger)
                {
                    new MainWindow().ShowDialog();
                }
            }
            catch (BO.BlAlreadyExistEntityException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (BO.BlEmptyStringException ex)// catching exceptions from Bl
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

  
   
}
  

using BlApi;
using BO;
using PL.Orders;
using PL.Product;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
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

        public BO.User? MyUser
        {
            get { return (BO.User)GetValue(MyUserProperty); }
            set { SetValue(MyUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyUserProperty =
            DependencyProperty.Register("MyUser", typeof(BO.User), typeof(Window), new PropertyMetadata(null));

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
            MyUser = new BO.User();
            InitializeComponent();
        }



        private void btnLogIn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                bl.User.Checking(MyUser);
                if (MyUser.LogIn == 0)
                {
                    myCart.CustomerName = MyUser.Name;
                    myCart.CustomerEmail = MyUser.Email;
                    new MainWindow(myCart).ShowDialog();
                }
                else
                {
                    new MainWindow().ShowDialog();
                }


            }
            catch (BO.BlAlreadyExistEntityException ex)
            {
                MessageBox.Show("You already in our system", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show("The Passowrd is worng ,please try again", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
           
            
        }

        private void btnSignIn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bl.User.AddUser(MyUser);
                MessageBox.Show("Wellcome to our store,ENJOY 🛍", "😀", MessageBoxButton.OK, MessageBoxImage.Information);

            }
           catch(BO.BlMissingEntityException ex)
            {
                MessageBox.Show("The Passowrd is worng ,please try again", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            catch(BO.BlEmptyStringException ex)
            {
                MessageBox.Show("Enter your details,please", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
            catch (BO.BlAlreadyExistEntityException ex)
            {

                MessageBox.Show("You already in our system", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
        }
    }



}
  

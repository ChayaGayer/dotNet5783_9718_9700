using BO;
using PL.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Simulator.xaml
    /// </summary>
    /// 
    public partial class Simulator : Window
    {

        public List<BO.OrderForList?> MyOrderList
        {
            get { return (List<BO.OrderForList?>)GetValue(MyOrderListProperty); }
            set { SetValue(MyOrderListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyOrderList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyOrderListProperty=
            DependencyProperty.Register("MyOrderList", typeof(List<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));


        BackgroundWorker worker;
        bool f=false;
        DateTime timeSim= DateTime.Now;
        BlApi.IBl bl = BlApi.Factory.Get();
        public Simulator()
        {
            InitializeComponent();
            MyOrderList = bl.Order.GetListedOrders().ToList();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            //MyOrderList = new List<BO.OrderForList?>(bl.Order.GetListedOrders());
           // orderForListDataGrid.ItemsSource = bl.Order.GetListedOrders();
        }



       
        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (f == true)
            {
                MessageBox.Show("ALL ORDERS DELIVERED");
            }
            else if (e.Cancelled == true)
            {
                MessageBox.Show(" Simulator Stoped");
            }
            this.Cursor = Cursors.Arrow;
        }

        


        //buttonCancel.Enabled = false;
        //buttonStart.Enabled = true;


        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
           


            var ordListTemp = new List<BO.OrderForList?>(bl.Order.GetListedOrders());


            foreach (var item in ordListTemp)
            {
                BO.Order orderSimulator = bl.Order.RequestOrderDeta(item?.ID ?? throw new NullReferenceException());
                if (timeSim - orderSimulator.OrderDate >= new TimeSpan(3, 0, 0, 0) && orderSimulator.Status == BO.OrderStatus.Ordered)
                    bl.Order.UpdateSendOrder(orderSimulator.ID);//, timeSim);
                if (timeSim - orderSimulator.OrderDate >= new TimeSpan(10, 0, 0, 0) && orderSimulator.Status == BO.OrderStatus.Shipped)
                    bl.Order.UpdateSupplyOrder(orderSimulator.ID);//, timeSim);

            }
            if (MyOrderList.All(x => x?.Status == BO.OrderStatus.Delivered))
            {
                if (worker.WorkerSupportsCancellation == true)
                    worker.CancelAsync(); // Cancel.
                f = true;//if all the orders delivered
            }
            MyOrderList = new List<BO.OrderForList?>(bl.Order.GetListedOrders());
            //MyOrderList = bl.Order.GetListedOrders().ToList();
            //  MyOrderList.ItemsSource = bl.Order.GetListedOrders();





        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
          
            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {

                    

                    timeSim = timeSim.AddHours(4);
                    Thread.Sleep(2000);
                    if (worker.WorkerReportsProgress == true)
                        worker.ReportProgress(11);

                }
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {

            if (!worker.IsBusy)
            {
                this.Cursor = Cursors.Wait;
                worker.RunWorkerAsync();
            }

          
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
            {
                worker.CancelAsync();
            }

        }

        private void OrderTrack_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? ofl = orderForListDataGrid.SelectedItem as BO.OrderForList;
            if (ofl != null)
            {
                OrderTrack orderTrackSim = new OrderTrack(ofl.ID);
                orderTrackSim.ShowDialog();
            }
        }
    }
}

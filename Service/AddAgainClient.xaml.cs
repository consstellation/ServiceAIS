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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Service
{
    /// <summary>
    /// Логика взаимодействия для AddAgainClient.xaml
    /// </summary>
    public partial class AddAgainClient : Page
    {

        Orders newOrder = new Orders();

        public AddAgainClient(Orders myOrder)
        {
            InitializeComponent();
            newOrder.Clients = myOrder.Clients;
            newOrder.Client_equip = new Client_equip(); 
        }

        private void LoadingPage(object sender, RoutedEventArgs e)
        {
            DataContext = newOrder;
            oldclient.DataContext = newOrder;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        { 
            
            navigat.frame.Navigate(new OrdersPage());

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(ln.Text) || string.IsNullOrWhiteSpace(fn.Text) || string.IsNullOrWhiteSpace(ln.Text) || string.IsNullOrWhiteSpace(patr.Text) || string.IsNullOrWhiteSpace(pas_ser.Text) || !deadline.SelectedDate.HasValue
                  || string.IsNullOrWhiteSpace(pas_num.Text) || string.IsNullOrWhiteSpace(phone.Text) || string.IsNullOrWhiteSpace(tq.Text) || string.IsNullOrWhiteSpace(phone.Text) || string.IsNullOrWhiteSpace(des_mod.Text))
                    throw new Exception("Данные не могут быть пустыми");

                // Дата и время оформления заказа.
                newOrder.Data_order = DateTime.Now;
                newOrder.Status = "Не готов";
                MainWindow.context.Orders.Add(newOrder);
                MainWindow.context.SaveChanges();
                MessageBox.Show("Успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                navigat.frame.Navigate(new OrdersPage());

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        private void phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void pas_num_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void pas_ser_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}

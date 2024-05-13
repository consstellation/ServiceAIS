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
using System.IO;
using Microsoft.Win32;

namespace Service
{
    /// <summary>
    /// Логика взаимодействия для AddPage.8xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private Orders currentOrder;

        public AddPage(Orders myOrder)
        {

            InitializeComponent();
            pas_num.MaxLength = 4;
            pas_ser.MaxLength = 6;
            pas_ser.Text = null;
            pas_num.Text = null;

            currentOrder = myOrder;

            if (currentOrder is null)
            {
                currentOrder = new Orders();
                currentOrder.Client_equip = new Client_equip();
                currentOrder.Clients = new Clients();
            }

            DataContext = currentOrder;

            if (myOrder == null)
            {

                ID.Visibility = Visibility.Hidden;
                idtext.Visibility = Visibility.Hidden;

                add.Content = "Добавить";
            }
            else
            {

                ID.Visibility = Visibility.Visible;
                idtext.Visibility = Visibility.Visible;
                add.Content = "Редактировать";
                zagol.Text = "Редактирование заказа клиента";
            }

            abd.DataContext = currentOrder;
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
                    throw new Exception("Основные данные не могут быть пустыми!");

                if (currentOrder.ID_client == 0)
                {
                    // Дата и время оформления заказа.
                    currentOrder.Data_order = DateTime.Now;
                    currentOrder.Status = "Не готов";
                    MainWindow.context.Orders.Add(currentOrder);
                    MainWindow.context.SaveChanges();           
                   
                }
                else
                {
                    MainWindow.context.SaveChanges();
                
                }

          
                MainWindow.context.SaveChanges();
                MessageBox.Show("Успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                navigat.frame.Navigate(new OrdersPage());
            }
       
            
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

        }

        private void pas_num_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void pas_ser_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }


        private void phone_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}

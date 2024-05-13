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
    /// Логика взаимодействия для SpecialistOrdersPage.xaml
    /// </summary>
    public partial class SpecialistOrdersPage : Page
    {
        private Users_service auth;
        public SpecialistOrdersPage()
        {
            InitializeComponent();
        }

        private void CloseOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите завершить заказ",
                "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            Orders order = (sender as Button).DataContext as Orders;
            if(order.Services is null == false)
            {
                order.Data_order1 = DateTime.Now;
                order.Status = "Готов";
                MainWindow.context.SaveChanges();
                MessageBox.Show("Заказ завершен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                GridSpecialistOrder.ItemsSource = MainWindow.context.Orders.Where(o => o.ID_specialist == auth.ID_specialist).ToList();
            }
            else
            {
                MessageBox.Show("Сначала выберите услугу", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
   

        }

        private void specload(object sender, RoutedEventArgs e)
        {
            auth = DataContext as Users_service; // получаем переданные данные специалиста.
            // Получаем заказы, данного специалиста.
            GridSpecialistOrder.ItemsSource = MainWindow.context.Orders.Where(order => order.ID_specialist == auth.ID_specialist).ToList();

            if (GridSpecialistOrder.Items.Count == 0)
                MessageBox.Show("В данный момент у вас нет заказов","Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void vibor_usl(object sender, RoutedEventArgs e)
        {
            Orders orders = (sender as Button).DataContext as Orders;
            if (orders.Status == "Готов")
            {
                MessageBox.Show("Вы закрыли заказ и не можете выбрать услугу еще раз", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                SelectedService selectedService = new SelectedService();
                // Передача заказа для закрепления за ним специалиста.
                selectedService.DataContext = (sender as Button).DataContext as Orders;
                selectedService.ShowDialog();

            }
    
        }



        public void Update()
        {
            var clientable = MainWindow.context.Orders.ToList();
            //поиск по именИ
            Clients aba = new Clients();
            //clientable = clientable.Where(a => aba.ID_client..StartsWith(Poisk.Text) || a.Last_name.StartsWith(Poisk.Text) ||
            //a.Patronymic.StartsWith(Poisk.Text) || a.Number_phone.StartsWith(Poisk.Text)).ToList();

            clientable = clientable.Where(a => a.Clients.First_name.StartsWith(Poisk.Text) || a.Clients.Last_name.StartsWith(Poisk.Text) || a.Clients.Patronymic.StartsWith(Poisk.Text) || a.Clients.Number_phone.StartsWith(Poisk.Text)).ToList();



            GridSpecialistOrder.ItemsSource = clientable;
            GridSpecialistOrder.Items.Refresh();
        }

        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}

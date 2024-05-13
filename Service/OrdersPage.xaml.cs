using System;
using System.Collections.Generic;
using System.Data;
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

    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            OrdersGrid.ItemsSource = MainWindow.context.Orders.ToList();
        }
        #region Order

        int count = 0;

        //подгрузка страницы
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OrdersGrid.Items.Refresh();
        }

        //перейти на страницу добавления клиента
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            navigat.frame.Navigate(new AddPage(null));
        }

        //редактирование заказа
        private void EDITBUT_CLICK(object sender, RoutedEventArgs e)
        {
            // передача данных заказа, чтобы из них вытащить данные клиента и данные оборудования.
            Orders order = (sender as Button).DataContext as Orders;

            if (order.Status == "Готов" && order.ID_Services != 0 && order.Guaran != null)
            {
                MessageBox.Show("Вы  не можете редактировать заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (count == 0)
                navigat.frame.Navigate(new AddPage((sender as Button).DataContext as Orders));


        }

        //удаление заказа
        //private void DELETE_CLICK(object sender, RoutedEventArgs e)
        //{
        //    Orders order = (sender as Button).DataContext as Orders;
        //    //if (order.Status == "Не готов")
        //    //{
        //    //    MessageBox.Show("Заказ без статуса \"Готов\" не может быть удален", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        //    //    return;
        //    //}

        //    if (MessageBox.Show("Вы действительно хотите удалить клиента?",
        //     "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
        //        return;

        //    try
        //    {
        //        MainWindow.context.Orders.Remove(order);

        //        var itemToRemoveCli = MainWindow.context.Clients.SingleOrDefault(x => x.ID_client == order.ID_client);
        //        MainWindow.context.Clients.Remove(itemToRemoveCli);

        //        var itemToRemoveEcli = MainWindow.context.Client_equip.SingleOrDefault(x => x.ID_equip == order.ID_equip);
        //        MainWindow.context.Client_equip.Remove(itemToRemoveEcli);
        //        MainWindow.context.SaveChanges();
        //        OrdersGrid.ItemsSource = MainWindow.context.Orders.ToList();
        //        MessageBox.Show("Успешно удалено!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Удаление не произведено. Завершите все заказы клиента", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }


        //}

        //повторное добавление заказа
        private void ADD_Click_again(object sender, RoutedEventArgs e)
        {

            navigat.frame.Navigate(new AddAgainClient((sender as Button).DataContext as Orders));

        }

        //назначение специалиста
        private void Submit_click(object sender, RoutedEventArgs e)
        {
            Orders orders = (sender as Button).DataContext as Orders;
            if (orders.Status == "Готов")
            {
                MessageBox.Show("Заказ готов. Специалист уже назначен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                Transfer transfer = new Transfer();
                // Передача заказа для закрепления за ним специалиста.
                transfer.DataContext = (sender as Button).DataContext as Orders;
                transfer.ShowDialog();
            }



        }

        //перенос зданных из таблицы в Word документ
        private void createdogovor_Click(object sender, RoutedEventArgs e)
        {
            Orders order = (sender as Button).DataContext as Orders;


            if (order.Status == "Готов" && order.ID_Services != 0 && order.Guaran != null)
            {

                var helper = new WordHelper("shablon.docx");
                var items = new Dictionary<string, string>
                {
                      {"<FIRNAM>",order.Clients.First_name},
                      {"<PATR>",order.Clients.Patronymic},
                      {"<LANAM>",order.Clients.Last_name},
                      {"<PASNUM>",order.Clients.Passport_number.ToString()},
                      {"<PASSER>",order.Clients.Passport_series.ToString()},
                      {"<PHONUM>",order.Clients.Number_phone.ToString()},
                      {"<TYOB>",order.Client_equip.Type_equip},
                      {"<BRAN>",order.Client_equip.Brand},
                      {"<MOD>",order.Client_equip.Model},
                      {"<PASSW>",order.Client_equip.Password_OS},
                      {"<PROBL>",order.Client_equip.Description},
                      {"<USL>",order.Services.Name},
                      {"<STOIM>",order.Services.Price.ToString()},
                      {"<CEN>",order.Services.Price.ToString()},
                      {"<KOL>",order.Services.Count.ToString()},
                      {"<GARA>",order.Guaran.ToString()},
                      {"<SROK>",order.Deadline.ToString()},
                      {"<DATAEND>",order.Data_order1.ToString()},
                      {"<DATA_ZAKAZA>",order.Data_order.ToString()},
                 };
                helper.Process(items);

                MessageBox.Show("Успешно. Документ создан.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Невозможно создать документ. Закончите заказ, выберите услугу и срок гарантии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }




        }

        //выбор гарантии
        private void guaran_Click(object sender, RoutedEventArgs e)
        {
            Orders order = (sender as Button).DataContext as Orders;

            if (order.Guaran != null)
            {

                MessageBox.Show("Вы уже обозначали гарантию", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }

            if (order.Status == "Готов")
            {
                Window window;
                window = new SelectedDate(((sender as Button).DataContext as Orders));
                window.Show();
            }

            else
            {
                MessageBox.Show("Заказ еще не готов", "Инфомарция", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


        }

        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }



        public void Update()
        {
            var clientable = MainWindow.context.Orders.ToList();
            Clients aba = new Clients();

           clientable = clientable.Where(a => a.Clients.First_name.StartsWith(Poisk.Text) || a.Clients.Last_name.StartsWith(Poisk.Text) || a.Clients.Patronymic.StartsWith(Poisk.Text) || a.Clients.Number_phone.StartsWith(Poisk.Text)).ToList();
           OrdersGrid.ItemsSource = clientable;
           OrdersGrid.Items.Refresh();
        }
        #endregion

       
    }
}

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

namespace Service
{
    /// <summary>
    /// Логика взаимодействия для SelectedService.xaml
    /// </summary>
    public partial class SelectedService : Window
    {
        public SelectedService()
        {
            InitializeComponent();
            //  usl.ItemsSource = MainWindow.context.List_spec.Where(spec => spec.Users_service.FirstOrDefault().Role == "Специалист").ToList();
            usl.ItemsSource = MainWindow.context.Services.ToList();
        }

        private void vyborusl_click(object sender, RoutedEventArgs e)
        {
            if (DataContext is null)
            {
                MessageBox.Show("Заказ не был выбран", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }

            Services serv = usl.SelectedItem as Services;
            Client_equip equip = new Client_equip();

   
            if (serv is null)
            {
                MessageBox.Show("Выберете услугу", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
           
            try
            {        
               
                Orders order = DataContext as Orders;             
                order.ID_Services = serv.ID_services;
                equip.Services.Add(serv);
                MainWindow.context.SaveChanges();
                MessageBox.Show("Услуга была выбрана успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                

               
            }
            catch
            {
                MessageBox.Show("Вы не выбрали услугу", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            finally
            {
                Close();
            }

        }
    }
}

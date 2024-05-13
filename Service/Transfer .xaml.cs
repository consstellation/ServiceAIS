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
    /// Логика взаимодействия для Transfer.xaml
    /// </summary>
    public partial class Transfer : Window
    {
        

        public Transfer()
        {
            InitializeComponent();
            gend.ItemsSource = MainWindow.context.List_spec.Where(spec => spec.Users_service.FirstOrDefault().Role == "Специалист").ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is null)
            {
                MessageBox.Show("Заказ не был выбран");
                Close();
                return;
            }

            List_spec specialist = gend.SelectedItem as List_spec;
            if (specialist is null)
            {
                MessageBox.Show("выберите специалиста", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                Orders order = DataContext as Orders;
                order.ID_specialist = specialist.ID_specialist;
                MainWindow.context.SaveChanges();
                MessageBox.Show("Специалист был выбран успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("произошла ошибка: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}

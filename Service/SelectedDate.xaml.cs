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
    /// Логика взаимодействия для SelectedDate.xaml
    /// </summary>
    public partial class SelectedDate : Window
    {

        private Orders myorders;

        public SelectedDate(Orders orders)
        { 
            InitializeComponent();
            myorders = orders;
        }

        private void selected_guarandate(object sender, RoutedEventArgs e)
        {
            try
            {
                myorders.Guaran = guarandate.SelectedDate.Value.Date;     
                MainWindow.context.SaveChanges();
                MessageBox.Show("Успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
               
            
               
            }
            catch
            {
                MessageBox.Show("Вы не выбрали дату", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            finally
            {
                Close();
            }

        }
    }
}

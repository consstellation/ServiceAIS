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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Users_service auth;


        public static Entities context = new Entities();

        public MainWindow()
        {
            InitializeComponent();
  
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButton.YesNo,MessageBoxImage.Information);
            if (res == MessageBoxResult.No) e.Cancel = true;
        }

        private void LoadedWindow(object sender, RoutedEventArgs e)
        {
            auth = DataContext as Users_service;
            navigat.frame = myframe;

            if (auth.Role == "Администратор")
            {
                myframe.Navigate(new OrdersPage());
            }
            else
            {
                SpecialistOrdersPage specialistOrdersPage = new SpecialistOrdersPage();
                specialistOrdersPage.DataContext = DataContext;
                myframe.Navigate(specialistOrdersPage);
            }
        }
    }
}

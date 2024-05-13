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
    /// Логика взаимодействия для LogPage.xaml
    /// </summary>
    public partial class LogPage : Window
    {
        public LogPage() => InitializeComponent();

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text,
            password = passwordBox.Password;


            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Все поля обязательны для заполнения", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Получить объект Users_service, у которого совпадает указанные логин и пароль 
            Users_service auth = MainWindow.context.Users_service.Where(user => user.Login == login && user.Password == password).FirstOrDefault();

            // Если auth пустой, значит комбинации логина&пароля нет в бд
            if (auth is null)
            {
                MessageBox.Show("Неправильный логин или пароль!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            /*Window window;
            if (auth.Role == "Администратор")
                window = new MainWindow();
            else
            {
                window = new SpecialWindow();
                window.DataContext = auth; // Сохраняем данные специалиста в новом окне, чтобы потом узнать его заказы.
            }*/

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = auth; 

            MessageBox.Show($"Добро пожаловать, {auth.List_spec.First_name}!\nВаша роль: {auth.Role}","Вход", MessageBoxButton.OK, MessageBoxImage.Information);
            mainWindow.Show();
            Close();
        }
    }
}

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
using ClientSupportApp.Data;

namespace ClientSupportApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var username = UsernameBox.Text.Trim();
                var password = PasswordBox.Password.Trim();

                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    // Сохраняем данные в сессию
                    UserSession.UserId = user.Id;
                    UserSession.Username = user.Username;
                    UserSession.Role = user.Role;

                    MessageBox.Show($"Добро пожаловать, {user.Username} ({user.Role})!", "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);

                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
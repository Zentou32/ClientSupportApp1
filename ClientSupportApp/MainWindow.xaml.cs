using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientSupportApp.Models;
using ClientSupportApp.Data;

namespace ClientSupportApp
{
    public partial class MainWindow : Window
    {
        private readonly User _currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Права доступа в зависимости от роли
            switch (_currentUser.Role)
            {
                case "Клиент":
                    AttendanceButton.IsEnabled = false;
                    AnalyticsButton.IsEnabled = false;
                    break;

                case "Оператор Поддержки":
                    AnalyticsButton.IsEnabled = false;
                    break;

                case "Супервайзер":
                    // Всё доступно, ничего отключать не надо
                    break;

                default:
                    // На всякий случай, если роль какая-то странная
                    AttendanceButton.IsEnabled = false;
                    AnalyticsButton.IsEnabled = false;
                    break;
            }
        }

        private void OpenClientRequests(object sender, RoutedEventArgs e)
        {
            ClientRequestsWindow window = new ClientRequestsWindow();
            window.Show();
        }

        private void OpenAchievements(object sender, RoutedEventArgs e)
        {
            AchievementsWindow window = new AchievementsWindow();
            window.Show();
        }

        private void OpenAttendance(object sender, RoutedEventArgs e)
        {
            AttendanceWindow window = new AttendanceWindow();
            window.Show();
        }

        private void OpenAnalytics(object sender, RoutedEventArgs e)
        {
            AnalyticsWindow window = new AnalyticsWindow();
            window.Show();
        }
    }
}
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
using ClientSupportApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientSupportApp
{
    public partial class ClientRequestsWindow : Window
    {
        public ClientRequestsWindow()
        {
            InitializeComponent();
            ConfigureByRole();
            LoadRequests();
        }

        private void ConfigureByRole()
        {
            // Если роль — Клиент, то редактировать и удалять НЕЛЬЗЯ
            if (UserSession.Role == "Клиент")
            {
                EditRequestButton.IsEnabled = false;
                DeleteRequestButton.IsEnabled = false;
            }
        }

        private void LoadRequests()
        {
            using var db = new AppDbContext();
            List<Request> list = db.Requests
                .Include(r => r.User)
                .ToList();
            RequestsGrid.ItemsSource = list;
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            var win = new RequestEditWindow();
            if (win.ShowDialog() == true)
                LoadRequests();
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            // (тут кнопка уже неактивна для Клиента, но дополнительной проверки не понадобится)
            if (RequestsGrid.SelectedItem is Request sel)
            {
                var win = new RequestEditWindow(sel);
                if (win.ShowDialog() == true)
                    LoadRequests();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is Request sel)
            {
                var result = MessageBox.Show(
                    $"Удалить обращение #{sel.RequestNumber}?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using var db = new AppDbContext();
                    var toDel = db.Requests.Find(sel.Id);
                    if (toDel != null)
                    {
                        db.Requests.Remove(toDel);
                        db.SaveChanges();
                    }
                    LoadRequests();
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

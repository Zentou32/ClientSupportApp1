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

namespace ClientSupportApp
{
    public partial class RequestEditWindow : Window
    {
        private readonly Request _request;
        private readonly bool _isEditMode;

        public RequestEditWindow(Request? request = null)
        {
            InitializeComponent();

            if (request != null)
            {
                // Редактирование существующего
                _request = request;
                _isEditMode = true;
                CategoryBox.Text = request.Category;
                DescriptionBox.Text = request.Description;
                RequestNumberBox.Text = request.RequestNumber;
            }
            else
            {
                // Создание нового: заполняем все required-поля пустыми значениями
                _request = new Request
                {
                    Category = string.Empty,
                    Description = string.Empty,
                    RequestNumber = string.Empty,
                    CreatedAt = DateTime.Now,
                    UserId = UserSession.UserId
                };
                _isEditMode = false;

                // Генерируем следующий номер
                using var db = new AppDbContext();
                int nextId = db.Requests.Any() ? db.Requests.Max(r => r.Id) + 1 : 1;
                string generatedNumber = nextId.ToString();

                RequestNumberBox.Text = generatedNumber;
                _request.RequestNumber = generatedNumber;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionBox.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new AppDbContext();

            if (_isEditMode)
            {
                var ent = db.Requests.Find(_request.Id);
                if (ent != null)
                {
                    ent.Category = CategoryBox.Text.Trim();
                    ent.Description = DescriptionBox.Text.Trim();
                    // Номер не меняем
                    db.SaveChanges();
                }
            }
            else
            {
                // У _request уже установлены все required-поля
                _request.Category = CategoryBox.Text.Trim();
                _request.Description = DescriptionBox.Text.Trim();
                _request.CreatedAt = DateTime.Now;

                db.Requests.Add(_request);
                db.SaveChanges();
            }

            DialogResult = true;
            Close();
        }
    }
}

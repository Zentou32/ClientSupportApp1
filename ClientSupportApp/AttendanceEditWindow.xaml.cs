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
    public partial class AttendanceEditWindow : Window
    {
        private readonly Attendance _attendance;
        private readonly bool _isEditMode;

        public AttendanceEditWindow(Attendance? record = null)
        {
            InitializeComponent();

            // Загружаем список пользователей в ComboBox
            using (var db = new AppDbContext())
            {
                var users = db.Users.ToList();
                UserComboBox.ItemsSource = users;
            }

            if (record != null)
            {
                _attendance = record;
                _isEditMode = true;

                // Предустанавливаем выбранного пользователя
                UserComboBox.SelectedValue = record.UserId;
                DatePickerVisited.SelectedDate = record.DateVisited;
                PresentCheckBox.IsChecked = record.WasPresent;
            }
            else
            {
                _attendance = new Attendance
                {
                    DateVisited = DateTime.Today,
                    WasPresent = true
                };
                _isEditMode = false;

                // По умолчанию ничего не выбрано, или можно выбрать текущего залогиненного
                // UserComboBox.SelectedValue = UserSession.UserId;
                DatePickerVisited.SelectedDate = _attendance.DateVisited;
                PresentCheckBox.IsChecked = _attendance.WasPresent;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (DatePickerVisited.SelectedDate == null)
            {
                MessageBox.Show("Укажите дату посещения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                if (_isEditMode)
                {
                    var rec = db.Attendances.Find(_attendance.Id);
                    if (rec != null)
                    {
                        rec.UserId = (int)UserComboBox.SelectedValue!;
                        rec.DateVisited = DatePickerVisited.SelectedDate.Value;
                        rec.WasPresent = PresentCheckBox.IsChecked == true;
                        db.SaveChanges();
                    }
                }
                else
                {
                    _attendance.UserId = (int)UserComboBox.SelectedValue!;
                    _attendance.DateVisited = DatePickerVisited.SelectedDate.Value;
                    _attendance.WasPresent = PresentCheckBox.IsChecked == true;

                    db.Attendances.Add(_attendance);
                    db.SaveChanges();
                }
            }

            DialogResult = true;
            Close();
        }
    }
}

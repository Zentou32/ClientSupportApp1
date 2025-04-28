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
    public partial class AchievementEditWindow : Window
    {
        private readonly Achievement _achievement;
        private readonly bool _isEditMode;

        public AchievementEditWindow(Achievement? achievement = null)
        {
            InitializeComponent();

            // Загружаем всех пользователей в ComboBox
            using (var db = new AppDbContext())
            {
                var users = db.Users.ToList();
                UserComboBox.ItemsSource = users;
            }

            if (achievement != null)
            {
                _achievement = achievement;
                _isEditMode = true;

                // Устанавливаем выбранного пользователя
                UserComboBox.SelectedValue = achievement.UserId;
                ContestBox.Text = achievement.ContestName;
                PointsBox.Text = achievement.Points.ToString();
                CertificateBox.Text = achievement.Certificate;
                DateAchievedPicker.SelectedDate = achievement.DateAchieved;
            }
            else
            {
                _achievement = new Achievement
                {
                    ContestName = string.Empty,
                    Points = 0,
                    Certificate = string.Empty,
                    DateAchieved = DateTime.Today
                };
                _isEditMode = false;

                // По умолчанию можно не выбирать никого или же выбрать текущего:
                // UserComboBox.SelectedValue = UserSession.UserId;
                DateAchievedPicker.SelectedDate = _achievement.DateAchieved;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.SelectedValue == null
                || string.IsNullOrWhiteSpace(ContestBox.Text)
                || string.IsNullOrWhiteSpace(PointsBox.Text)
                || string.IsNullOrWhiteSpace(CertificateBox.Text)
                || DateAchievedPicker.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите клиента.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(PointsBox.Text.Trim(), out int pts))
            {
                MessageBox.Show("Баллы должны быть числом.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new AppDbContext();

            if (_isEditMode)
            {
                var item = db.Achievements.Find(_achievement.Id);
                if (item != null)
                {
                    item.UserId = (int)UserComboBox.SelectedValue!;
                    item.ContestName = ContestBox.Text.Trim();
                    item.Points = pts;
                    item.Certificate = CertificateBox.Text.Trim();
                    item.DateAchieved = DateAchievedPicker.SelectedDate.Value;
                    db.SaveChanges();
                }
            }
            else
            {
                _achievement.UserId = (int)UserComboBox.SelectedValue!;
                _achievement.ContestName = ContestBox.Text.Trim();
                _achievement.Points = pts;
                _achievement.Certificate = CertificateBox.Text.Trim();
                _achievement.DateAchieved = DateAchievedPicker.SelectedDate.Value;

                db.Achievements.Add(_achievement);
                db.SaveChanges();
            }

            DialogResult = true;
            Close();
        }
    }
}

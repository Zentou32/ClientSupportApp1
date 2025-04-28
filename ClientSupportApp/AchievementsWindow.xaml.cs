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
    public partial class AchievementsWindow : Window
    {
        public AchievementsWindow()
        {
            InitializeComponent();

            // Сначала настраиваем доступность кнопок по ролям
            ConfigureByRole();

            // Затем загружаем и отображаем данные
            LoadAchievements();
        }

        private void ConfigureByRole()
        {
            // Клиент может только просматривать — все CRUD-кнопки отключаем
            if (UserSession.Role == "Клиент")
            {
                AddAchievementButton.IsEnabled = false;
                EditAchievementButton.IsEnabled = false;
                DeleteAchievementButton.IsEnabled = false;
            }
        }

        private void LoadAchievements()
        {
            using var db = new AppDbContext();
            // Подгружаем вместе с User, чтобы клиентское имя было доступно
            List<Achievement> list = db.Achievements
                .Include(a => a.User)
                .ToList();

            AchievementsGrid.ItemsSource = list;
        }

        private void AddAchievement_Click(object sender, RoutedEventArgs e)
        {
            var win = new AchievementEditWindow();
            if (win.ShowDialog() == true)
                LoadAchievements();
        }

        private void EditAchievement_Click(object sender, RoutedEventArgs e)
        {
            if (AchievementsGrid.SelectedItem is Achievement sel)
            {
                var win = new AchievementEditWindow(sel);
                if (win.ShowDialog() == true)
                    LoadAchievements();
            }
            else
            {
                MessageBox.Show("Выберите запись.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteAchievement_Click(object sender, RoutedEventArgs e)
        {
            if (AchievementsGrid.SelectedItem is Achievement sel)
            {
                var result = MessageBox.Show(
                    $"Удалить достижение '{sel.ContestName}'?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using var db = new AppDbContext();
                    var toDel = db.Achievements.Find(sel.Id);
                    if (toDel != null)
                    {
                        db.Achievements.Remove(toDel);
                        db.SaveChanges();
                    }
                    LoadAchievements();
                }
            }
            else
            {
                MessageBox.Show("Выберите запись.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
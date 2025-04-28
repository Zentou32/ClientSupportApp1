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
    public partial class AnalyticsWindow : Window
    {
        public AnalyticsWindow()
        {
            InitializeComponent();
            LoadAnalytics();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadAnalytics();
        }

        private void LoadAnalytics()
        {
            using var db = new AppDbContext();
            // Грузим нужные данные
            var users = db.Users.AsNoTracking().ToList();
            var attendances = db.Attendances.AsNoTracking().ToList();
            var requests = db.Requests.AsNoTracking().ToList();
            var achievements = db.Achievements.AsNoTracking().ToList();

            var data = new List<AnalyticsModel>();

            foreach (var u in users)
            {
                var userAtt = attendances.Where(a => a.UserId == u.Id);
                int totalSes = userAtt.Count();
                int presentSes = userAtt.Count(a => a.WasPresent);
                double rate = totalSes > 0
                    ? Math.Round(presentSes * 100.0 / totalSes, 1)
                    : 0.0;

                int reqCount = requests.Count(r => r.UserId == u.Id);
                int achPoints = achievements
                    .Where(a => a.UserId == u.Id)
                    .Sum(a => a.Points);

                // Рейтинг: здесь простая формула, можно скорректировать по-бизнес-требованиям
                double rating = Math.Round(rate + reqCount + achPoints, 1);

                data.Add(new AnalyticsModel
                {
                    Username = u.Username,
                    TotalSessions = totalSes,
                    PresentSessions = presentSes,
                    AttendanceRate = rate,
                    RequestCount = reqCount,
                    AchievementPoints = achPoints,
                    Rating = rating
                });
            }

            // Сортируем по рейтингу по убыванию
            AnalyticsGrid.ItemsSource = data
                .OrderByDescending(x => x.Rating)
                .ToList();
        }
    }
}

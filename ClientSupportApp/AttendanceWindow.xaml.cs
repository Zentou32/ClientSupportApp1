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
    public partial class AttendanceWindow : Window
    {
        public AttendanceWindow()
        {
            InitializeComponent();
            LoadAttendance();
        }

        private void LoadAttendance()
        {
            using var db = new AppDbContext();
            List<Attendance> items = db.Attendances
                .Include(a => a.User)
                .ToList();
            AttendanceGrid.ItemsSource = items;
        }

        private void AddAttendance_Click(object sender, RoutedEventArgs e)
        {
            var win = new AttendanceEditWindow();
            if (win.ShowDialog() == true)
                LoadAttendance();
        }

        private void EditAttendance_Click(object sender, RoutedEventArgs e)
        {
            if (AttendanceGrid.SelectedItem is Attendance sel)
            {
                var win = new AttendanceEditWindow(sel);
                if (win.ShowDialog() == true)
                    LoadAttendance();
            }
            else
            {
                MessageBox.Show("Выберите запись посещаемости для редактирования.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteAttendance_Click(object sender, RoutedEventArgs e)
        {
            if (AttendanceGrid.SelectedItem is Attendance sel)
            {
                var ans = MessageBox.Show(
                    $"Удалить запись для {sel.ClientName} от {sel.DateVisited:dd.MM.yyyy}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ans == MessageBoxResult.Yes)
                {
                    using var db = new AppDbContext();
                    var toDel = db.Attendances.Find(sel.Id);
                    if (toDel != null)
                    {
                        db.Attendances.Remove(toDel);
                        db.SaveChanges();
                    }
                    LoadAttendance();
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSupportApp.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }    // Навигационное свойство

        public DateTime DateVisited { get; set; }
        public bool WasPresent { get; set; }

        public string ClientName => User?.Username ?? "Не указано";
        public string PresentText => WasPresent ? "Да" : "Нет";
    }
}

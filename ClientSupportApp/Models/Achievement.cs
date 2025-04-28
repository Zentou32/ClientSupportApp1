using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSupportApp.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }       // Навигационное свойство

        public required string ContestName { get; set; }
        public int Points { get; set; }
        public required string Certificate { get; set; }
        public DateTime DateAchieved { get; set; }

        // Вычисляемое имя клиента
        public string ClientName => User?.Username ?? "Не указано";
    }
}

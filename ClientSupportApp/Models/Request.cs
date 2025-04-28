using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSupportApp.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; } // Навигационное свойство
        public required string Category { get; set; }
        public required string Description { get; set; }
        public required string RequestNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        // Вычисляемое имя клиента
        public string ClientName => User?.Username ?? "Не указано";
    }
}

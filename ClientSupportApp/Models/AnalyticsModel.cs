using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSupportApp.Models
{
    public class AnalyticsModel
    {
        public string Username { get; set; } = string.Empty;
        public int TotalSessions { get; set; }
        public int PresentSessions { get; set; }
        public double AttendanceRate { get; set; }   // в процентах
        public int RequestCount { get; set; }
        public int AchievementPoints { get; set; }
        public double Rating { get; set; }
    }
}

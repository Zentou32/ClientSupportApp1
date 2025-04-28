using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSupportApp
{
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string Username { get; set; } = string.Empty;
        public static string Role { get; set; } = string.Empty;
    }
}


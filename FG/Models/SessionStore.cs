using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FG.Models
{
    public class SessionStore
    {
        public int LoginAttempts { get; set; }
        public DateTime LastLoginAttempt { get; set; }
        public Usuario Usuario { get; set; }
    }
}
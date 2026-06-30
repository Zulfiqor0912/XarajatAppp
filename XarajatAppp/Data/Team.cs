using System;
using System.Collections.Generic;
using System.Text;

namespace XarajatAppp.Data
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
    }
}

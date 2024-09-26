using Microsoft.AspNetCore.Identity;
using System;

namespace Sakkinny.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime? LastLoginDate { get; set; } // Nullable to allow default value
        public bool ReceiveNewsletter { get; internal set; }
        public string PreferredLanguage { get; internal set; }
    }
}

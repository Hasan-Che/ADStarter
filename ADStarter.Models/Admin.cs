#nullable disable
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Admin
    {
        [Key]
        public int a_ID { get; set; }

        [StringLength(100)]
        public string a_name { get; set; }

        public IdentityUser User { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}

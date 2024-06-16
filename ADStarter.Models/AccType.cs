#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models
{
    public partial class AccType
    {
        [Key]
        public int atype_ID { get; set; }

        [StringLength(100)]
        public string atype_desc { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}

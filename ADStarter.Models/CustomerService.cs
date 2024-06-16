#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class CustomerService
    {
        [Key]
        public int cs_ID { get; set; }

        [StringLength(100)]
        public string cs_name { get; set; }

        public int acc_ID { get; set; }

        [ForeignKey(nameof(acc_ID))]
        public virtual Account Account { get; set; }
    }
}

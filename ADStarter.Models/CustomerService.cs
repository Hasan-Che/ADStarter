#nullable disable
using Microsoft.AspNetCore.Identity;
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

        public IdentityUser User { get; set; }


    }
}

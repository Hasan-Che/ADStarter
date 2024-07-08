#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Announcement
    {
        [Key]
        public int ann_ID { get; set; }

        public int a_ID { get; set; }

        [StringLength(100)]
        public string ann_title { get; set; }

        public string ann_desc { get; set; }
        public string ann_status { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey(nameof(a_ID))]
        public virtual Admin Admin { get; set; }
    }
}

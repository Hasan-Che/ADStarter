#nullable disable
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public class ParentChildViewModel
    {
        public Parent Parent { get; set; }
        public Child Child { get; set; }
        public TreatmentHistory TreatmentHistory { get; set; }
        public IdentityUser UserId { get; set; }  // Add this property
        public int parent_ID { get; set; }  // Add this property
    }
}

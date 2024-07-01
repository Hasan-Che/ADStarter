#nullable disable
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Schedule
    {
        public object therapist_ID;

        [Key]
        public int schedule_ID { get; set; }

        // Removed property
        // public int session_ID { get; set; }

        public int? t_ID { get; set; }

        public DateTime session_datetime { get; set; }

        public int prog_ID { get; set; }

        public int slot_ID { get; set; }

        public string c_myKid { get; set; }

       
        [ForeignKey(nameof(t_ID))]
        public virtual Therapist? Therapist { get; set; }

        [ForeignKey(nameof(prog_ID))]
        public virtual Program? Program { get; set; }

        [ForeignKey(nameof(slot_ID))]
        public virtual Slot Slot { get; set; }

        [ForeignKey(nameof(c_myKid))]
        public virtual Child Child { get; set; }

        // Added navigation property for Report
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}

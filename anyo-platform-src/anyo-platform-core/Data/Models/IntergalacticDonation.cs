using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace anyo_platform.Models
{
    public class IntergalacticDonation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Message { get; set; }

        [DefaultValue(1)]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        public int MissionId { get; set; }


        [ForeignKey("Donor")]
        public string DonorId { get; set; }
        public virtual IdentityUser? Donor { get; set; }


        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public virtual IntergalacticPackages? Package { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
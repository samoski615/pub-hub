﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PubHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ApplicationId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [NotMapped]
        public bool IsBarOwner { get; set; }
    }
}

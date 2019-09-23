using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PubHub.Models
{
    public class BarOwner
    {
        [Key]
        public int BarOwnerId { get; set; }

        [Display(Name = "Bar Name")]
        public string BarName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; } 

        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }

        //public double Latitude { get; set; }
        //public double Longitude { get; set; }

        [Display(Name = "Bar Atmosphere")]
        public string TypeOfBar { get; set; }

        [Display(Name = "Average Customer Rating")]
        public int AverageRating { get; set; }

        [Display(Name = "Bar open: ")]
        public string BarOpen { get; set; }

        [Display(Name = "Bar close: ")]
        public string BarClose { get; set; }

        [Display(Name = "Happy Hour start time:")]
        public string HappyHourStartTime { get; set; }

        [Display(Name = "Happy Hour end time:")]
        public string HappyHourEndTime { get; set; }

        [Display(Name = "Customers Checked In")]
        public int PotentialCustomers { get; set; }

        [Display(Name = "Type of Drink")]
        public string TypeOfDrink { get; set; }

        [NotMapped]
        [Display(Name = "Day of the Week")]
        public SelectList DayOfWeek { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //public int HappyHourSpecialsId { get; set; }
        //public HappyHourSpecials HappyHourSpecials { get; set; }

    }
}

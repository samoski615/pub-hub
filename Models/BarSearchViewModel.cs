using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubHub.Models
{
    public class BarSearchViewModel
    {
        [Key]
        public int Id { get; set; }
        public List<BarOwner> BarOwners { get; set; }

        public SelectList TypeOfBar { get; set; }

        public SelectList Zipcode { get; set; }

        public string SearchString { get; set; }

        [Display(Name = "Day of the Week")]
        public SelectList DayOfWeek { get; set; }

        [Display(Name = "Type of Drink")]
        public SelectList TypeOfDrink { get; set; }

        [Display(Name = "Drink Price")]
        public double DrinkPrice { get; set; }

        [Display(Name = "Happy Hour Start Time")]
        public SelectList HappyHourStartTime { get; set; }

        [Display(Name = "Happy Hour End Time")]
        public SelectList HappyHourEndTime { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PubHub.Models
{
    public class HappyHourSpecials
    {
        [Key]
        public int HappyHourSpecialsId { get; set; }

        [Display(Name = "Day of the Week")]
        public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "Type of Drink")]
        public string TypeOfDrink { get; set; }

        [Display(Name = "Drink Price")]
        public double DrinkPrice { get; set; }

        [Display(Name = "Happy Hour Start Time")]
        public string HappyHourStartTime { get; set; }

        [Display(Name = "Happy Hour End Time")]
        public string HappyHourEndTime { get; set; }

    }
}

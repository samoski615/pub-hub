using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubHub.Models
{
    public class RatingsTable
    {
        [Key]
        public int RatingsTableId { get; set; }
        public int BarOwnerId { get; set; }
        public int DrinkEnthusiastId { get; set; }
        public Ratings CustomerRating { get; set; }
        public enum Ratings
        {
            one, two, three, four, five
        }
    }
}

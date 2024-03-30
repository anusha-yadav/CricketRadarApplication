using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketRadarApplication.Models
{
    public class Teams
    {
        public int TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? City { get; set; }
        public string? Captain { get; set; }
    }
}

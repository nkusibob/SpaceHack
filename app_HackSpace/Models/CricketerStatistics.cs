using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace app_HackSpace.Models
{
    public class CricketerStatistics
    {
        [Required]
        public int OdiRuns { get; set; }
        [Required]
        public int TestRuns { get; set; }
        [Required]
        public int Century { get; set; }
        [Required]
        public int HalfCentury { get; set; }
        [Required]
        public int Wickets { get; set; }
        [Required]
        public int Catches { get; set; }
    }
}
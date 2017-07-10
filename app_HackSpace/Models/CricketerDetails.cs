using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace app_HackSpace.Models
{
    public class CricketerDetails
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name_ { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public string Team { get; set; }
        [Required]
        public bool IsSelected { get; set; }
    }
}
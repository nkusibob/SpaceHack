using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace app_HackSpace.Models
{
    public class ManageExplore
    {
        [Required]
        [Display(Name = "Title")]
        public string titleExplore { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string descExplore { get; set; }
    }

    public class ManageDeliver
    {
        [Required]
        [Display(Name = "Title")]
        public string titleDeliver { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string descDeliver { get; set; }
    }

    public class ManagePitch
    {
        [Required]
        [Display(Name = "Title")]
        public string titlePitch { get; set; }
    }

    public class ManageLaunch
    {
        [Required]
        [Display(Name = "Title")]
        public string titleLaunch { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string descLaunch { get; set; }
    }
}
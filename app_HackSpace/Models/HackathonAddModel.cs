using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace app_HackSpace.Models
{
    public class AddTheme
    {
        [Required]
        [Display(Name = "Name Theme")]
        public string nameTheme { get; set; }
        [Required]
        [Display(Name = "Description Theme")]
        public string descTheme { get; set; }
    }

    public class AddProgram
    {
        [Required]
        [Display(Name = "Name Program")]
        public string nameProg { get; set; }
        [Required]
        [Display(Name = "Description Program")]
        public string descProg { get; set; }
    }

    public class AddLocation
    {
        [Required]
        [Display(Name = "Name Location")]
        public string nameLoc { get; set; }
        [Required]
        [Display(Name = "Description Location")]
        public string descLoc { get; set; }
    }

    public class AddReward
    {
        [Required]
        [Display(Name = "Name Reward")]
        public string nameReward { get; set; }
        [Required]
        [Display(Name = "Description Reward")]
        public string descReward { get; set; }
    }

    public class AddGoodie
    {
        [Required]
        [Display(Name = "Name Goodie")]
        public string nameGoodie { get; set; }
        [Required]
        [Display(Name = "Description Goodie")]
        public string descGoodie { get; set; }
    }
}
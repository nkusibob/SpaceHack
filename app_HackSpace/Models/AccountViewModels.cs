using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.WebPages.Html;

namespace app_HackSpace.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Mémoriser ce navigateur ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique *")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe *")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe *")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        //-----ADDED>>>>>-----------------------------------------------------------------------------------------
        [Required]
        [Display(Name = "First name *")]
        public string firstname { get; set; }

        [Required]
        [Display(Name = "Last name *")]
        public string lastname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number *")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Type *")]
        public int id_Type { get; set; }

        [Display(Name = "Picture")]
        public string picture { get; set; }

        [Display(Name = "Company")]
        public string company { get; set; }

        [Required]
        [Display(Name = "Skills *")]
        public string[] idSkills { get; set; }
        //public IEnumerable<Skill> idSkills { get; set; }

        [Required]
        [Display(Name = "Size T-shirt *")]
        public int idSize_tshirt { get; set; }

        [Required]
        [Display(Name = "Food preferences *")]
        public int idFood_pref { get; set; }

        //-----PROFILS
        //expert
        [Display(Name = "Expertise")]
        public string expertise { get; set; }

        [Display(Name = "Availabilities")]
        public string availabilitiesEx { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Bio")]
        public string bioEx { get; set; }

        [Display(Name = "Hourly rate")]
        public string hourly_rateEx { get; set; }

        //sponsor
        [Display(Name = "Logo")]
        public string logoSp { get; set; }

        [Display(Name = "Maximum budget proposal")]
        public string max_budgetSp { get; set; }

        [Display(Name = "Preferred thematic")]
        public string prefer_thematicSp { get; set; }

        [Display(Name = "Needs")]
        public string needsSp { get; set; }

        //facilitator
        [Display(Name = "Availabilities")]
        public string availabilitiesFa { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Bio")]
        public string bioFa { get; set; }

        [Display(Name = "Hourly rate")]
        public string hourly_rateFa { get; set; }

        //[Url]
        [Display(Name = "Web site")]
        public string website { get; set; }

        //[Url]
        [Display(Name = "Facebook")]
        public string urlFcb { get; set; }

        //[Url]
        [Display(Name = "Twitter")]
        public string urlTwit { get; set; }

        //[Url]
        [Display(Name = "LinkedIn")]
        public string urlLin { get; set; }

        //super hero
        [Display(Name = "Availabilities")]
        public string availabilitiesSu { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Bio")]
        public string bioSu { get; set; }

        [Display(Name = "Hourly rate")]
        public string hourly_rateSu { get; set; }

        //jury
        [Display(Name = "Availabilities")]
        public string availabilitiesJu { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Bio")]
        public string bioJu { get; set; }

        [Display(Name = "Hourly rate")]
        public string hourly_rateJu { get; set; }

        //partner
        [Display(Name = "Logo")]
        public string logoPa { get; set; }

        [Display(Name = "Reward proposal")]
        public string reward_proposal { get; set; }

        [Display(Name = "Preferred thematic")]
        public string prefer_thematicPa { get; set; }

        [Display(Name = "Needs")]
        public string needsPa { get; set; }
        //-----<<<<<ADDED-----------------------------------------------------------------------------------------

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}

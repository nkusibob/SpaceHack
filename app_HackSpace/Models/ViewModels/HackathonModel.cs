using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace app_HackSpace.Models.ViewModels
{
    public class HackathonModel
    {
        public IEnumerable<Hackathon> hackathon { get; set; }

        public IEnumerable<Facilitator> facilitator { get; set; }
        public IEnumerable<Expert> expert { get; set; }
        public IEnumerable<Sponsor> sponsor { get; set; }
        public IEnumerable<Theme> theme { get; set; }
        public IEnumerable<Program> program { get; set; }
        public IEnumerable<Location_> location { get; set; }
        public IEnumerable<Reward> reward { get; set; }
        public IEnumerable<Goodie> goodie { get; set; }
        public IEnumerable<Payment> payment { get; set; }

        public IEnumerable<has_expert> Has_expert { get; set; }
        public IEnumerable<has_sponsor> Has_sponsor { get; set; }
        public IEnumerable<has_reward> Has_reward { get; set; }
        public IEnumerable<has_goodie> Has_goodie { get; set; }

        [Required]
        [Display(Name = "Name Hackathon")]
        public string nameHack { get; set; }
    }

    public class HackathonModelViewBag
    {
        public string NameFacil { get; set; }
        public string NameTheme { get; set; }
        public string NameProg { get; set; }
        public string NameLoc { get; set; }
        public string NameExpert { get; set; }
        public string NameReward { get; set; }
        public string NameSponsor { get; set; }
        public string NameGoodie { get; set; }
    }

    public class HackathonList
    {
        public IList<int> idExpertList { get; set; }
        public IList<int> idSponsorList { get; set; }
        public IList<int> idRewardList { get; set; }
        public IList<int> idGoodieList { get; set; }
    }
}
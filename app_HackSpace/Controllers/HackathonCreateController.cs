using app_HackSpace.CustomFilters;
using app_HackSpace.Models;
using app_HackSpace.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace app_HackSpace.Controllers
{
    [Authorize]
    public class HackathonCreateController : Controller
    {
        private HackSpaceDBEntities db = new HackSpaceDBEntities();

        // GET: HackathonCreate
        //[AuthorizeCustom(Roles = "User")]
        [Authorize]
        public ActionResult CreateHack()
        {
            return View("CreateHack");
        }

        //Sessions
        private Facilitator GetFacilitator()
        {
            if (Session["facilitator"] == null)
            {
                Session["facilitator"] = new Facilitator();
            }
            return (Facilitator)Session["facilitator"];
        }

        private void RemoveFacilitator()
        {
            Session.Remove("facilitator");
        }

        private Theme GetTheme()
        {
            if (Session["theme"] == null)
            {
                Session["theme"] = new Theme();
            }
            return (Theme)Session["theme"];
        }

        private void RemoveTheme()
        {
            Session.Remove("theme");
        }

        private Program GetProgram()
        {
            if (Session["program"] == null)
            {
                Session["program"] = new Program();
            }
            return (Program)Session["program"];
        }

        private void RemoveProgram()
        {
            Session.Remove("program");
        }

        private Location_ GetLocation()
        {
            if (Session["location"] == null)
            {
                Session["location"] = new Location_();
            }
            return (Location_)Session["location"];
        }

        private void RemoveLocation()
        {
            Session.Remove("location");
        }

        private HackathonList GetHackathonList()
        {
            if (Session["HackathonList"] == null)
            {
                Session["HackathonList"] = new HackathonList();
            }
            return (HackathonList)Session["HackathonList"];
        }

        private void RemoveHackathonList()
        {
            Session.Remove("HackathonList");
        }

        //ViewBag
        private HackathonModelViewBag GetViewBag()
        {
            if (Session["viewbag"] == null)
            {
                Session["viewbag"] = new HackathonModelViewBag();
            }
            return (HackathonModelViewBag)Session["viewbag"];
        }

        private void RemoveViewBag()
        {
            Session.Remove("viewbag");
        }
        //--------------------------------------------------------------------------------------------------------------
        // GET: HackathonCreate
        [Authorize]
        public ActionResult FacilitatorList()
        {
            return View(db.Facilitators.ToList());
        }
        // POST: HackathonCreate
        [HttpPost]
        [Authorize]
        public ActionResult FacilitatorList(List<Facilitator> facilList, string btnNext)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in facilList)
            {
                if (item.IsSelected)
                {
                    if (btnNext != null)
                    {
                        if (ModelState.IsValid)
                        {
                            Facilitator facilitatorObj = GetFacilitator();
                            facilitatorObj.idFacilitator = item.idFacilitator;

                            sb.Append(item.C_Profile.lastname + " " + item.C_Profile.firstname);
                            HackathonModelViewBag viewbagObj = GetViewBag();
                            viewbagObj.NameFacil = sb.ToString();

                            return RedirectToAction("ThemeList");
                        }
                    }
                }
            }
            return View("FacilitatorList");
        }

        //public ActionResult SaveFacilitatorHack(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Facilitator facilitator = db.Facilitators.Find(id);
        //    if (facilitator == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Facilitator facilitatorObj = GetFacilitator();
        //        facilitatorObj.idFacilitator = facilitator.idFacilitator;
        //        facilitatorObj.C_Profile.firstname = facilitator.C_Profile.firstname;

        //        return RedirectToAction("ThemeList");
        //    }

        //    return RedirectToAction("FacilitatorList");
        //}
        //--------------------------------------------------------------------------------------------------------------

        // GET: HackathonCreate
        [Authorize]
        public ActionResult ThemeList()
        {
            HackathonModelViewBag viewbagObj = GetViewBag();
            ViewBag.nameFacil = "Facilitator : " + viewbagObj.NameFacil;

            return View(db.Themes.ToList());
        }
        // POST: HackathonCreate
        [Authorize]
        [HttpPost]
        public ActionResult ThemeList(List<Theme> themeList, string btnPrevious, string btnNext)
        {
            StringBuilder sb = new StringBuilder();
            if (btnPrevious != null)
            {
                return RedirectToAction("FacilitatorList");
            }

            foreach (var item in themeList)
            {
                if (item.IsSelected)
                {
                    if (btnNext != null)
                    {
                        if (ModelState.IsValid)
                        {
                            Theme themeObj = GetTheme();
                            themeObj.idTheme = item.idTheme;

                            sb.Append(item.nameTheme);
                            HackathonModelViewBag viewbagObj = GetViewBag();
                            viewbagObj.NameTheme = sb.ToString();

                            return RedirectToAction("ProgramList");
                        }
                    }
                }
            }
            return View("ThemeList");
        }

        //public ActionResult SaveThemeHack(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Theme theme = db.Themes.Find(id);
        //    if (theme == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Theme themeObj = GetTheme();
        //        themeObj.idTheme = theme.idTheme;
        //        themeObj.nameTheme = theme.nameTheme;

        //        return RedirectToAction("ProgramList");
        //    }

        //    return RedirectToAction("ThemeList");
        //}
        //-----------------------------------------------------------------------------------------------------------------

        // GET: HackathonCreate
        [Authorize]
        public ActionResult ProgramList()
        {
            HackathonModelViewBag viewbagObj = GetViewBag();
            ViewBag.nameFacil = "Facilitator : " + viewbagObj.NameFacil;
            ViewBag.nameTheme = "Theme : " + viewbagObj.NameTheme;

            return View(db.Programs.ToList());
        }
        // POST: HackathonCreate
        [Authorize]
        [HttpPost]
        public ActionResult ProgramList(List<Program> programList, string btnPrevious, string btnNext)
        {
            StringBuilder sb = new StringBuilder();

            if (btnPrevious != null)
            {
                return RedirectToAction("ThemeList");
            }

            foreach (var item in programList)
            {
                if (item.IsSelected)
                {
                    if (btnNext != null)
                    {
                        if (ModelState.IsValid)
                        {
                            Program programObj = GetProgram();
                            programObj.idProg = item.idProg;

                            sb.Append(item.nameProg);
                            HackathonModelViewBag viewbagObj = GetViewBag();
                            viewbagObj.NameProg = sb.ToString();

                            return RedirectToAction("LocationList");
                        }
                    }
                }
            }
            return View("ProgramList");
        }

        //public ActionResult SaveProgramHack(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Program program = db.Programs.Find(id);
        //    if (program == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Program programObj = GetProgram();
        //        programObj.idProg = program.idProg;
        //        programObj.nameProg = program.nameProg;

        //        return RedirectToAction("LocationList");
        //    }

        //    return RedirectToAction("ProgramList");
        //}
        //-------------------------------------------------------------------------------------------------------------------

        // GET: HackathonCreate
        [Authorize]
        public ActionResult LocationList()
        {
            HackathonModelViewBag viewbagObj = GetViewBag();
            ViewBag.nameFacil = "Facilitator : " + viewbagObj.NameFacil;
            ViewBag.nameTheme = "Theme : " + viewbagObj.NameTheme;
            ViewBag.nameProg = "Program : " + viewbagObj.NameProg;

            return View(db.Location_.ToList());
        }
        // POST: HackathonCreate
        [Authorize]
        [HttpPost]
        public ActionResult LocationList(List<Location_> locationList, string btnPrevious, string btnNext)
        {
            StringBuilder sb = new StringBuilder();

            if (btnPrevious != null)
            {
                return RedirectToAction("ProgramList");
            }

            foreach (var item in locationList)
            {
                if (item.IsSelected)
                {
                    if (btnNext != null)
                    {
                        if (ModelState.IsValid)
                        {
                            Location_ locationObj = GetLocation();
                            locationObj.idLoc = item.idLoc;

                            sb.Append(item.nameLoc);
                            HackathonModelViewBag viewbagObj = GetViewBag();
                            viewbagObj.NameLoc = sb.ToString();

                            return RedirectToAction("ExpertList");
                        }
                    }
                }
            }
            return View("LocationList");
        }

        //public ActionResult SaveLocationHack(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Location_ location = db.Location_.Find(id);
        //    if (location == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Location_ locationObj = GetLocation();
        //        locationObj.idLoc = location.idLoc;
        //        locationObj.nameLoc = location.nameLoc;

        //        return RedirectToAction("ExpertList");
        //    }

        //    return RedirectToAction("LocationList");
        //}
        //--------------------------------------------------------------------------------------------------------------------

        // GET: HackathonCreate
        [Authorize]
        public ActionResult ExpertList()
        {
            HackathonModelViewBag viewbagObj = GetViewBag();
            ViewBag.nameFacil = "Facilitator : " + viewbagObj.NameFacil;
            ViewBag.nameTheme = "Theme : " + viewbagObj.NameTheme;
            ViewBag.nameProg = "Program : " + viewbagObj.NameProg;
            ViewBag.nameLoc = "Location : " + viewbagObj.NameLoc;

            return View(db.Experts.ToList());
        }
        // POST: HackathonCreate
        [Authorize]
        [HttpPost]
        public ActionResult ExpertList(List<Expert> expertList, string btnPrevious, string btnNext)
        {
            StringBuilder sb = new StringBuilder();

            if (btnPrevious != null)
            {
                return RedirectToAction("LocationList");
            }

            HackathonList hasExpertObj = GetHackathonList();
            hasExpertObj.idExpertList = new List<int>();

            foreach (var item in expertList)
            {
                if (item.IsSelected)
                {
                    if (btnNext != null)
                    {
                        if (ModelState.IsValid)
                        {
                            hasExpertObj.idExpertList.Add(item.idExpert);

                            sb.Append(item.C_Profile.lastname + " " + item.C_Profile.firstname + ", ");
                            HackathonModelViewBag viewbagObj = GetViewBag();
                            viewbagObj.NameExpert = sb.ToString();
                        }
                    }
                }
            }
            return RedirectToAction("RewardList");
        }

        //public ActionResult SaveExpertHack(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Expert expert = db.Experts.Find(id);
        //    if (expert == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Expert expertObj = GetExpert();
        //        expertObj.idExpert = expert.idExpert;
        //        expertObj.C_Profile.firstname = expert.C_Profile.firstname;

        //        return RedirectToAction("RewardList");
        //    }

        //    return RedirectToAction("ExpertList");
        //}

        //--------------------------------------------------------------------------------------------------------------------
        // GET: HackathonCreate
        [Authorize]
        public ActionResult RewardList()
        {
            HackathonModelViewBag viewbagObj = GetViewBag();
            ViewBag.nameFacil = "Facilitator : " + viewbagObj.NameFacil;
            ViewBag.nameTheme = "Theme : " + viewbagObj.NameTheme;
            ViewBag.nameProg = "Program : " + viewbagObj.NameProg;
            ViewBag.nameLoc = "Location : " + viewbagObj.NameLoc;
            ViewBag.nameExpert = "Expert : " + viewbagObj.NameExpert;

            return View(db.Rewards.ToList());
        }
        // POST: HackathonCreate
        [Authorize]
        [HttpPost]
        public ActionResult RewardList(List<Reward> rewardList, string btnPrevious, string btnNext)
        {
            StringBuilder sb = new StringBuilder();

            if (btnPrevious != null)
            {
                return RedirectToAction("ExpertList");
            }

            HackathonList hasRewardObj = GetHackathonList();
            hasRewardObj.idRewardList = new List<int>();

            foreach (var item in rewardList)
            {
                if (item.IsSelected)
                {
                    if (btnNext != null)
                    {
                        if (ModelState.IsValid)
                        {
                            hasRewardObj.idRewardList.Add(item.idReward);

                            sb.Append(item.nameReward + ", ");
                            HackathonModelViewBag viewbagObj = GetViewBag();
                            viewbagObj.NameReward = sb.ToString();
                        }
                    }
                }
            }
            return RedirectToAction("SponsorList");
        }

        //public ActionResult SaveRewardHack(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Reward reward = db.Rewards.Find(id);
        //    if (reward == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Reward rewardObj = GetReward();
        //        rewardObj.idReward = reward.idReward;
        //        rewardObj.nameReward = reward.nameReward;

        //        return RedirectToAction("SponsorList");
        //    }

        //    return RedirectToAction("RewardList");
        //}
        //--------------------------------------------------------------------------------------------------------------------

        // GET: HackathonCreate
        [Authorize]
        public ActionResult SponsorList()
        {
            HackathonModelViewBag viewbagObj = GetViewBag();
            ViewBag.nameFacil = "Facilitator : " + viewbagObj.NameFacil;
            ViewBag.nameTheme = "Theme : " + viewbagObj.NameTheme;
            ViewBag.nameProg = "Program : " + viewbagObj.NameProg;
            ViewBag.nameLoc = "Location : " + viewbagObj.NameLoc;
            ViewBag.nameExpert = "Expert : " + viewbagObj.NameExpert;
            ViewBag.nameReward = "Reward : " + viewbagObj.NameReward;

            return View(db.Sponsors.ToList());
        }
        // POST: HackathonCreate
        [Authorize]
        [HttpPost]
        public ActionResult SponsorList(List<Sponsor> sponsorList, string btnPrevious, string btnNext)
        {
            StringBuilder sb = new StringBuilder();

            if (btnPrevious != null)
            {
                return RedirectToAction("RewardList");
            }

            HackathonList hasSponsorObj = GetHackathonList();
            hasSponsorObj.idSponsorList = new List<int>();

            foreach (var item in sponsorList)
            {
                if (item.IsSelected)
                {
                    if (btnNext != null)
                    {
                        if (ModelState.IsValid)
                        {
                            hasSponsorObj.idSponsorList.Add(item.idSponsor);

                            sb.Append(item.C_Profile.lastname + " " + item.C_Profile.firstname + ", ");
                            HackathonModelViewBag viewbagObj = GetViewBag();
                            viewbagObj.NameSponsor = sb.ToString();
                        }
                    }
                }
            }
            return RedirectToAction("GoodieList");
        }

        //public ActionResult SaveSponsorHack(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Sponsor sponsor = db.Sponsors.Find(id);
        //    if (sponsor == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Sponsor sponsorObj = GetSponsor();
        //        sponsorObj.idSponsor = sponsor.idSponsor;
        //        sponsorObj.C_Profile.firstname = sponsor.C_Profile.firstname;

        //        return RedirectToAction("GoodieList");
        //    }

        //    return RedirectToAction("SponsorList");
        //}
        //--------------------------------------------------------------------------------------------------------------------

        // GET: HackathonCreate
        [Authorize]
        public ActionResult GoodieList()
        {
            HackathonModelViewBag viewbagObj = GetViewBag();
            ViewBag.nameFacil = "Facilitator : " + viewbagObj.NameFacil;
            ViewBag.nameTheme = "Theme : " + viewbagObj.NameTheme;
            ViewBag.nameProg = "Program : " + viewbagObj.NameProg;
            ViewBag.nameLoc = "Location : " + viewbagObj.NameLoc;
            ViewBag.nameExpert = "Expert : " + viewbagObj.NameExpert;
            ViewBag.nameReward = "Reward : " + viewbagObj.NameReward;
            ViewBag.nameSponsor = "Sponsor : " + viewbagObj.NameSponsor;

            return View(db.Goodies.ToList());
        }
        // POST: HackathonCreate
        [Authorize]
        [HttpPost]
        public ActionResult GoodieList(List<Goodie> goodieList, string btnPrevious, string btnNext)
        {
            StringBuilder sb = new StringBuilder();

            if (btnPrevious != null)
            {
                return RedirectToAction("SponsorList");
            }

            HackathonList hasGoodieObj = GetHackathonList();
            hasGoodieObj.idGoodieList = new List<int>();

            foreach (var item in goodieList)
            {
                if (item.IsSelected)
                {
                    if (btnNext != null)
                    {
                        if (ModelState.IsValid)
                        {
                            hasGoodieObj.idGoodieList.Add(item.idGoodie);

                            sb.Append(item.nameGoodie + ", ");
                            HackathonModelViewBag viewbagObj = GetViewBag();
                            viewbagObj.NameGoodie = sb.ToString();
                        }
                    }
                }
            }
            return RedirectToAction("Confirm");
        }

        //public ActionResult SaveGoodieHack(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Goodie goodie = db.Goodies.Find(id);
        //    if (goodie == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Goodie goodieObj = GetGoodie();
        //        goodieObj.idGoodie = goodie.idGoodie;
        //        goodieObj.nameGoodie = goodie.nameGoodie;

        //        return RedirectToAction("Confirm");
        //    }

        //    return RedirectToAction("GoodieList");
        //}
        //--------------------------------------------------------------------------------------------------------------------

        // GET: HackathonCreate
        [Authorize]
        public ActionResult Confirm()
        {
            HackathonModelViewBag viewbagObj = GetViewBag();
            ViewBag.nameFacil = "Facilitator : " + viewbagObj.NameFacil;
            ViewBag.nameTheme = "Theme : " + viewbagObj.NameTheme;
            ViewBag.nameProg = "Program : " + viewbagObj.NameProg;
            ViewBag.nameLoc = "Location : " + viewbagObj.NameLoc;
            ViewBag.nameExpert = "Expert : " + viewbagObj.NameExpert;
            ViewBag.nameReward = "Reward : " + viewbagObj.NameReward;
            ViewBag.nameSponsor = "Sponsor : " + viewbagObj.NameSponsor;
            ViewBag.nameGoodie = "Goodie : " + viewbagObj.NameGoodie;

            return View("Confirm");
        }
        // GET: HackathonCreate
        [Authorize]
        public ActionResult SaveAllHack(HackathonModel model, string btnPrevious, string btnNext)
        {
            Facilitator facilitatorObj = GetFacilitator();
            Theme themeObj = GetTheme();
            Program programObj = GetProgram();
            Location_ locationObj = GetLocation();
            HackathonList hasHackListObj = GetHackathonList();

            if (btnPrevious != null)
            {
                return RedirectToAction("GoodieList");
            }

            if (btnNext != null)
            {
                if (ModelState.IsValid)
                {
                    Hackathon h = new Hackathon();
                    h.idFacilitator = facilitatorObj.idFacilitator;
                    h.idTheme = themeObj.idTheme;
                    h.idProg = programObj.idProg;
                    h.idLoc = locationObj.idLoc;

                    h.nameHack = model.nameHack;

                    h.UserName = User.Identity.Name;

                    db.Hackathons.Add(h);
                    db.SaveChanges();

                    //int latestHackId = h.idHackathon;

                    has_expert hasExpert = new has_expert();
                    foreach (var e in hasHackListObj.idExpertList)
                    {
                        int latestHackId = h.idHackathon;

                        hasExpert.idExpert = e;
                        hasExpert.idHackathon = latestHackId;

                        db.has_expert.Add(hasExpert);
                        db.SaveChanges();
                    }

                    has_reward hasReward = new has_reward();
                    foreach (var r in hasHackListObj.idRewardList)
                    {
                        int latestHackId = h.idHackathon;

                        hasReward.idReward = r;
                        hasReward.idHackathon = latestHackId;

                        db.has_reward.Add(hasReward);
                        db.SaveChanges();
                    }

                    has_sponsor hasSponsor = new has_sponsor();
                    foreach (var s in hasHackListObj.idSponsorList)
                    {
                        int latestHackId = h.idHackathon;

                        hasSponsor.idSponsor = s;
                        hasSponsor.idHackathon = latestHackId;

                        db.has_sponsor.Add(hasSponsor);
                        db.SaveChanges();
                    }

                    has_goodie hasGoodie = new has_goodie();
                    foreach (var g in hasHackListObj.idGoodieList)
                    {
                        int latestHackId = h.idHackathon;

                        hasGoodie.idGoodie = g;
                        hasGoodie.idHackathon = latestHackId;

                        db.has_goodie.Add(hasGoodie);
                        db.SaveChanges();
                    }

                    RemoveFacilitator();
                    RemoveTheme();
                    RemoveProgram();
                    RemoveLocation();
                    RemoveHackathonList();
                    RemoveViewBag();

                    return View("Success");
                }
            }

            return RedirectToAction("Confirm");
        }

        //------------------------------------------------------------------------------------------------------------------
        [Authorize]
        public ActionResult IndexHome()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
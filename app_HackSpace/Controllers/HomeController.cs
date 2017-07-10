using app_HackSpace.CustomFilters;
using app_HackSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Net;
using app_HackSpace.Models.ViewModels;
using Microsoft.WindowsAzure.Storage.Blob;

namespace app_HackSpace.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private HackSpaceDBEntities db = new HackSpaceDBEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var hackathons = db.Hackathons.Include(h => h.Facilitator).Include(h => h.Location_).Include(h => h.Payment).Include(h => h.Program).Include(h => h.Theme);
            return View(hackathons.ToList());
        }

        [AllowAnonymous]
        public ActionResult SelectHack(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new HackathonModel();

            ViewBag.hackathonId = id.Value;

            var idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
            var nameHack = idHack.nameHack;
            ViewBag.nameHack = nameHack;

            var has_expert = db.has_expert.Include(e => e.Expert).Include(e => e.Hackathon).Where(e => e.idHackathon == id.Value);
            var has_sponsor = db.has_sponsor.Include(s => s.Sponsor).Include(s => s.Hackathon).Where(s => s.idHackathon == id.Value);
            var has_reward = db.has_reward.Include(r => r.Reward).Include(r => r.Hackathon).Where(r => r.idHackathon == id.Value);
            var has_goodie = db.has_goodie.Include(g => g.Goodie).Include(g => g.Hackathon).Where(g => g.idHackathon == id.Value);
            var theme = db.Hackathons.Include(t => t.Theme).Where(t => t.idHackathon == id.Value);
            var program = db.Hackathons.Include(p => p.Program).Where(p => p.idHackathon == id.Value);
            var location = db.Hackathons.Include(l => l.Location_).Where(l => l.idHackathon == id.Value);

            viewModel.Has_expert = has_expert;
            viewModel.Has_sponsor = has_sponsor;
            viewModel.Has_reward = has_reward;
            viewModel.Has_goodie = has_goodie;
            viewModel.hackathon = theme;
            viewModel.hackathon = program;
            viewModel.hackathon = location;

            return View(viewModel);
        }

        [Authorize]
        public ActionResult RegisterHack(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
            var nameHack = idHack.nameHack;
            ViewBag.nameHack = nameHack;

            ViewBag.hackathonId = id.Value;

            var userName = db.Participants.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            if (userName == null)
            {
                return RedirectToAction("RegisterConfirm", new { id = id });
            }

            else if (userName.UserName != null)
            {
                var participantId = userName.idParticipant;

                var hasParticipant = db.has_participant.Where(x => x.idHackathon == id).Where(y => y.idParticipant == participantId).FirstOrDefault();

                if (hasParticipant != null)
                {
                    return RedirectToAction("RedirectHack", new { id = id });
                }

                else if (ModelState.IsValid)
                {
                    return RedirectToAction("RegisterConfirm", new { id = id });
                }
            }

            return View("Index");
        }

        [Authorize]
        public ActionResult RegisterHack2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Participant p = new Participant();
            has_participant has_p = new has_participant();

            var userName = db.Participants.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            if (userName == null)
            {
                if (ModelState.IsValid)
                {
                    p.UserName = User.Identity.Name;

                    db.Participants.Add(p);
                    db.SaveChanges();

                    int latestParticipId = p.idParticipant;

                    has_p.idHackathon = id.Value;
                    has_p.idParticipant = latestParticipId;

                    db.has_participant.Add(has_p);
                    db.SaveChanges();

                    return RedirectToAction("Explore", "Explore", new { id = id });
                }
            }

            else if (userName.UserName != null)
            {
                var participantId = userName.idParticipant;

                var hasParticipant = db.has_participant.Where(x => x.idHackathon == id).Where(y => y.idParticipant == participantId).FirstOrDefault();

                if (hasParticipant != null)
                {
                    return RedirectToAction("Explore", "Explore", new { id = id });
                }

                else if (ModelState.IsValid)
                {
                    has_p.idHackathon = id.Value;
                    has_p.idParticipant = userName.idParticipant;

                    db.has_participant.Add(has_p);
                    db.SaveChanges();

                    return RedirectToAction("Explore", "Explore", new { id = id });
                }
            }

            return View("Index");
        }

        [Authorize]
        public ActionResult RegisterConfirm(int? id)
        {
            var idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
            ViewBag.nameHack = idHack.nameHack;

            ViewBag.hackathonId = id.Value;

            return View("RegisterConfirm");
        }

        [Authorize]
        public ActionResult RegisterConfirm2(int? id)
        {
            return RedirectToAction("Explore", "Explore", new { id = id });
        }

        [Authorize]
        public ActionResult RedirectHack(int? id)
        {
            var idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
            ViewBag.nameHack = idHack.nameHack;

            ViewBag.hackathonId = id.Value;

            return View("RedirectHack");
        }

        [Authorize]
        public ActionResult RedirectHack2(int? id)
        {
            return RedirectToAction("Explore", "Explore", new { id = id });
        }

        //[Authorize]
        //public ActionResult RegisterSuccess(int? id)
        //{
        //    ViewBag.hackathonId = id.Value;

        //    return View();
        //}

        [Authorize]
        public ActionResult Organize()
        {
            ViewBag.Message = "";

            return View();
        }

        [Authorize]
        public ActionResult Participate()
        {
            ViewBag.Message = "";

            return View();
        }

        [Authorize]
        public ActionResult Support()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}

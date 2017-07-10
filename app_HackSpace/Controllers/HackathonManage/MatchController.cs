using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using app_HackSpace.Models;
using app_HackSpace.Models.ViewModels;

namespace app_HackSpace.Controllers.HackathonManage
{
    [Authorize]
    public class MatchController : Controller
    {
        private HackSpaceDBEntities db = new HackSpaceDBEntities();

        // GET: Match
        [Authorize]
        public ActionResult Match(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hackathon idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
            var nameHack = idHack.nameHack;
            ViewBag.nameHack = nameHack;

            //var viewModel = new ManageModel();

            ViewBag.hackathonId = id.Value;

            var has_participant = db.has_participant.Include(p => p.Hackathon).Where(p => p.idHackathon == id.Value);

            //viewModel.has_participant = has_participant;

            return View(has_participant.ToList());

            //var has_participant = db.has_participant.Include(h => h.Hackathon).Include(h => h.Participant);
            //return View(has_participant.ToList());
        }



        // GET: Match/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            has_participant has_participant = db.has_participant.Find(id);
            if (has_participant == null)
            {
                return HttpNotFound();
            }
            return View(has_participant);
        }

        // GET: Match/Create
        public ActionResult Create()
        {
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "UserName");
            ViewBag.idParticipant = new SelectList(db.Participants, "idParticipant", "UserName");
            return View();
        }

        // POST: Match/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idhas_participant,idParticipant,idHackathon")] has_participant has_participant)
        {
            if (ModelState.IsValid)
            {
                db.has_participant.Add(has_participant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "UserName", has_participant.idHackathon);
            ViewBag.idParticipant = new SelectList(db.Participants, "idParticipant", "UserName", has_participant.idParticipant);
            return View(has_participant);
        }

        // GET: Match/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            has_participant has_participant = db.has_participant.Find(id);
            if (has_participant == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "UserName", has_participant.idHackathon);
            ViewBag.idParticipant = new SelectList(db.Participants, "idParticipant", "UserName", has_participant.idParticipant);
            return View(has_participant);
        }

        // POST: Match/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idhas_participant,idParticipant,idHackathon")] has_participant has_participant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(has_participant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "UserName", has_participant.idHackathon);
            ViewBag.idParticipant = new SelectList(db.Participants, "idParticipant", "UserName", has_participant.idParticipant);
            return View(has_participant);
        }

        // GET: Match/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            has_participant has_participant = db.has_participant.Find(id);
            if (has_participant == null)
            {
                return HttpNotFound();
            }
            return View(has_participant);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            has_participant has_participant = db.has_participant.Find(id);
            db.has_participant.Remove(has_participant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RedirectSelectHack(int? id)
        {
            return RedirectToAction("SelectHack", "Home", new { id = id });
        }

        public ActionResult RedirectExplore(int? id)
        {
            return RedirectToAction("Explore", "Explore", new { id = id });
        }

        public ActionResult RedirectPitch(int? id)
        {
            return RedirectToAction("Pitch", "Pitch", new { id = id });
        }

        public ActionResult RedirectDeliver(int? id)
        {
            return RedirectToAction("Deliver", "Deliver", new { id = id });
        }

        public ActionResult RedirectLaunch(int? id)
        {
            return RedirectToAction("Launch", "Launch", new { id = id });
        }
    }
}

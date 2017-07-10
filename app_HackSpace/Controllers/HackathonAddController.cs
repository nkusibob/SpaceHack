using app_HackSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app_HackSpace.Controllers
{
    [Authorize]
    public class HackathonAddController : Controller
    {
        private HackSpaceDBEntities db = new HackSpaceDBEntities();

        // GET: HackathonAdd
        [Authorize]
        public ActionResult ThemeAdd()
        {
            return View();
        }

        // POST: HackathonAdd
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemeAdd([Bind(Include = "idTheme,nameTheme,descTheme,IsSelected")] AddTheme theme)
        {
            Theme t = new Theme();
            if (ModelState.IsValid)
            {
                t.nameTheme = theme.nameTheme;
                t.descTheme = theme.descTheme;
                t.IsSelected = false;
                db.Themes.Add(t);
                db.SaveChanges();
                return RedirectToAction("ThemeList", "HackathonCreate");
            }

            return View(theme);
        }

        // GET: HackathonAdd
        [Authorize]
        public ActionResult ProgramAdd()
        {
            return View();
        }

        // POST: HackathonAdd
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProgramAdd([Bind(Include = "idProg,nameProg,descProg,IsSelected")] AddProgram program)
        {
            Program p = new Program();
            if (ModelState.IsValid)
            {
                p.nameProg = program.nameProg;
                p.descProg = program.descProg;
                p.IsSelected = false;
                db.Programs.Add(p);
                db.SaveChanges();
                return RedirectToAction("ProgramList", "HackathonCreate");
            }

            return View(program);
        }

        // GET: HackathonAdd
        [Authorize]
        public ActionResult LocationAdd()
        {
            return View();
        }

        // POST: HackathonAdd
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocationAdd([Bind(Include = "idLoc,nameLoc,descLoc,IsSelected")] AddLocation location_)
        {
            Location_ l = new Location_();
            if (ModelState.IsValid)
            {
                l.nameLoc = location_.nameLoc;
                l.descLoc = location_.descLoc;
                l.IsSelected = false;
                db.Location_.Add(l);
                db.SaveChanges();
                return RedirectToAction("LocationList", "HackathonCreate");
            }

            return View(location_);
        }

        // GET: HackathonAdd
        [Authorize]
        public ActionResult RewardAdd()
        {
            return View();
        }

        // POST: HackathonAdd
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RewardAdd([Bind(Include = "idReward,nameReward,descReward,IsSelected")] AddReward reward)
        {
            Reward r = new Reward();
            if (ModelState.IsValid)
            {
                r.nameReward = reward.nameReward;
                r.descReward = reward.descReward;
                r.IsSelected = false;
                db.Rewards.Add(r);
                db.SaveChanges();
                return RedirectToAction("RewardList", "HackathonCreate");
            }

            return View(reward);
        }

        // GET: GoodieTest/Create
        [Authorize]
        public ActionResult GoodieAdd()
        {
            return View();
        }

        // POST: GoodieTest/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GoodieAdd([Bind(Include = "idGoodie,nameGoodie,descGoodie,IsSelected")] AddGoodie goodie)
        {
            Goodie g = new Goodie();
            if (ModelState.IsValid)
            {
                g.nameGoodie = goodie.nameGoodie;
                g.descGoodie = goodie.descGoodie;
                g.IsSelected = false;
                db.Goodies.Add(g);
                db.SaveChanges();
                return RedirectToAction("GoodieList", "HackathonCreate");
            }

            return View(goodie);
        }
    }
}
using app_HackSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app_HackSpace.Controllers
{
    public class MyCricketerController : Controller
    {
        //
        // GET: /MyCricketer/
        public ActionResult Index()
        {
            return View("CricketerDetails");
        }

        private Cricketer GetCricketer()
        {
            if (Session["cricketer"] == null)
            {
                Session["cricketer"] = new Cricketer();
            }
            return (Cricketer)Session["cricketer"];
        }

        private void RemoveCricketer()
        {
            Session.Remove("cricketer");
        }

        [HttpPost]
        public ActionResult CricketerDetails(CricketerDetails DetailsData, string BtnPrevious, string BtnNext)
        {
            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    Cricketer CricObj = GetCricketer();
                    CricObj.ID = DetailsData.ID;
                    CricObj.Name_ = DetailsData.Name_;
                    CricObj.FullName = DetailsData.FullName;
                    CricObj.Age = DetailsData.Age;
                    CricObj.Team = DetailsData.Team;
                    CricObj.IsSelected = DetailsData.IsSelected;
                    return View("CricketerStatistics");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult CricketerStatistics(CricketerStatistics StatisticsData, string BtnPrevious, string BtnNext)
        {
            Cricketer CricObj = GetCricketer();
            if (BtnPrevious != null)
            {
                CricketerDetails DetailsObj = new CricketerDetails();
                DetailsObj.ID = CricObj.ID;
                DetailsObj.Name_ = CricObj.Name_;
                DetailsObj.FullName = CricObj.FullName;
                DetailsObj.Age = CricObj.Age;
                DetailsObj.Team = CricObj.Team;
                DetailsObj.IsSelected = CricObj.IsSelected;
                return View("CricketerDetails", DetailsObj);
            }
            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    CricObj.OdiRuns = StatisticsData.OdiRuns;
                    CricObj.TestRuns = StatisticsData.TestRuns;
                    CricObj.Century = StatisticsData.Century;
                    CricObj.HalfCentury = StatisticsData.HalfCentury;
                    CricObj.Wickets = StatisticsData.Wickets;
                    CricObj.Catches = StatisticsData.Catches;
                    HackSpaceDBEntities1 db = new HackSpaceDBEntities1();
                    db.Cricketers.Add(CricObj);
                    db.SaveChanges();
                    RemoveCricketer();
                    return View("Success");
                }
            }
            return View();
        }
    }
}
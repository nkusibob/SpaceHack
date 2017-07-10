using app_HackSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Net;
using app_HackSpace.Models.ViewModels;
using Microsoft.WindowsAzure.Storage.Blob;

namespace app_HackSpace.Controllers.HackathonManage
{
    [Authorize]
    public class LaunchController : Controller
    {
        private HackSpaceDBEntities db = new HackSpaceDBEntities();

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("image/png") || contentType.Equals("image/gif") || contentType.Equals("image/jpg") || contentType.Equals("image/jpeg");
        }

        private bool isValidContentLength(int contentLength)
        {
            return (contentLength / 3145728) <= 1; //3145728  octets = 3 Mo
        }

        //-----AZURE STORAGE>>>>>---------------------------------------------------------------------------
        BlobServicesLaunch _blobServices = new BlobServicesLaunch();
        // GET: HackathonManage/Launch
        [Authorize]
        public ActionResult Launch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hackathon idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
            var nameHack = idHack.nameHack;
            ViewBag.nameHack = nameHack;

            ViewBag.hackathonId = id.Value;

            var viewModel = new ManageModel();
            var launch = db.Launches.Include(l => l.Hackathon).Where(l => l.idHackathon == id.Value);
            viewModel.launch = launch;

            return View(viewModel);
        }

        [Authorize]
        public ActionResult LaunchUpload(string nameHack, int? id)
        {
            ViewBag.nameHack = nameHack;
            ViewBag.hackathonId = id.Value;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LaunchUpload(HttpPostedFileBase image, ManageLaunch model, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!isValidContentType(image.ContentType))
            {
                ViewBag.Error = "Only JPG, JPEG, PNG & GIF files are allowed.";
                return View("LaunchUpload");
            }
            if (!isValidContentLength(image.ContentLength))
            {
                ViewBag.Error = "Your file is too large. Maximum file size = 3 MB.";
                return View("LaunchUpload");
            }

            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                if (file.ContentLength == 0)
                    continue;

                if (file.ContentLength > 0)
                {
                    CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(file.FileName);
                    blob.UploadFromStream(file.InputStream);

                    string urlBlob = blobContainer.Uri.ToString();
                    string fileName = Path.GetFileName(image.FileName);
                    string urlCombine = urlBlob + "/" + fileName;

                    Launch l = new Launch();
                    l.title = model.titleLaunch;
                    l.descLaunch = model.descLaunch;
                    l.logo = urlCombine;
                    l.idHackathon = (int)id;

                    db.Launches.Add(l);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Launch", new { id = id });
        }
        //-----<<<<<AZURE STORAGE---------------------------------------------------------------------------

        // GET: HackathonManage/Launch
        //[Authorize]
        //public ActionResult Launch(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Hackathon idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
        //    var nameHack = idHack.nameHack;
        //    ViewBag.nameHack = nameHack;

        //    var viewModel = new ManageModel();

        //    ViewBag.hackathonId = id.Value;

        //    var launch = db.Launches.Include(l => l.Hackathon).Where(l => l.idHackathon == id.Value);

        //    viewModel.launch = launch;

        //    return View(viewModel);
        //}

        //[Authorize]
        //public ActionResult LaunchUpload(string nameHack, int? id)
        //{
        //    ViewBag.nameHack = nameHack;
        //    ViewBag.hackathonId = id.Value;
        //    return View();
        //}

        // POST: HackathonManage/Launch
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LaunchUpload(HttpPostedFileBase image, ManageLaunch model, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    if (!isValidContentType(image.ContentType))
        //    {
        //        ViewBag.Error = "Only JPG, JPEG, PNG & GIF files are allowed.";
        //        return View("LaunchUpload");
        //    }
        //    else if (!isValidContentLength(image.ContentLength))
        //    {
        //        ViewBag.Error = "Your file is too large. Maximum file size = 3 MB.";
        //        return View("LaunchUpload");
        //    }
        //    else
        //    {
        //        if (image.ContentLength > 0)
        //        {
        //            string fileName = Path.GetFileName(image.FileName);
        //            string path = Path.Combine(Server.MapPath("~/Content/Images/Launch/"), fileName);
        //            image.SaveAs(path);
        //            ViewBag.fileName = image.FileName;
        //            string imgUrl = image.FileName;

        //            if (ModelState.IsValid)
        //            {
        //                Launch l = new Launch();
        //                l.title = model.titleLaunch;
        //                l.descLaunch = model.descLaunch;
        //                l.logo = imgUrl;
        //                l.idHackathon = (int)id;

        //                db.Launches.Add(l);
        //                db.SaveChanges();
        //                return RedirectToAction("Launch", new { id = id });
        //            }
        //        }
        //    }
        //    return View("LaunchUpload", new { id = id });
        //}

        // GET: Launch/Edit
        [Authorize]
        public ActionResult LaunchEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Launch launch = db.Launches.Find(id);
            if (launch == null)
            {
                return HttpNotFound();
            }
            ViewBag.LaunchId = launch.idHackathon;
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "nameHack", launch.idHackathon);
            return View(launch);
        }

        // POST: Launch/Edit
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LaunchEdit([Bind(Include = "idLaunch,title,descLaunch,logo,idHackathon")] Launch launch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(launch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Launch", new { id = launch.idHackathon });
            }
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "nameHack", launch.idHackathon);
            return View(launch);
        }

        // GET: Launch/Delete
        [Authorize]
        public ActionResult LaunchDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Launch launch = db.Launches.Find(id);
            if (launch == null)
            {
                return HttpNotFound();
            }
            ViewBag.LaunchId = launch.idHackathon;
            return View(launch);
        }

        // POST: Launch/Delete
        [Authorize]
        [HttpPost, ActionName("LaunchDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult LaunchDeleteConfirmed(int id, string url)
        {
            Launch launch = db.Launches.Find(id);
            db.Launches.Remove(launch);
            db.SaveChanges();

            Uri uri = new Uri(url);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
            blob.Delete();

            return RedirectToAction("Launch", new { id = launch.idHackathon });
        }

        [Authorize]
        public ActionResult RedirectSelectHack(int? id)
        {
            return RedirectToAction("SelectHack", "Home", new { id = id });
        }

        [Authorize]
        public ActionResult RedirectExplore(int? id)
        {
            return RedirectToAction("Explore", "Explore", new { id = id });
        }

        [Authorize]
        public ActionResult RedirectPitch(int? id)
        {
            return RedirectToAction("Pitch", "Pitch", new { id = id });
        }

        [Authorize]
        public ActionResult RedirectMatch(int? id)
        {
            return RedirectToAction("Match", "Match", new { id = id });
        }

        [Authorize]
        public ActionResult RedirectDeliver(int? id)
        {
            return RedirectToAction("Deliver", "Deliver", new { id = id });
        }
    }
}
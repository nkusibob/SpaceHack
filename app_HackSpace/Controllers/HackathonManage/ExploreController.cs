using app_HackSpace.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using app_HackSpace.Models.ViewModels;
using Microsoft.WindowsAzure.Storage.Blob;

namespace app_HackSpace.Controllers.Manage
{
    [Authorize]
    public class ExploreController : Controller
    {
        private HackSpaceDBEntities db = new HackSpaceDBEntities();

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("video/mp4") || contentType.Equals("video/ogg");
        }

        private bool isValidContentLength(int contentLength)
        {
            return (contentLength / 100000000) <= 1; //100000000 octets = 100 Mo
        }

        //-----AZURE STORAGE>>>>>---------------------------------------------------------------------------
        BlobServicesExplore _blobServices = new BlobServicesExplore();
        // GET: HackathonManage/Explore
        [Authorize]
        public ActionResult Explore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
            var nameHack = idHack.nameHack;
            ViewBag.nameHack = nameHack;

            ViewBag.hackathonId = id.Value;

            var viewModel = new ManageModel();
            var explore = db.Explores.Where(e => e.idHackathon == id.Value);
            viewModel.explore = explore;

            return View(viewModel);
        }

        [Authorize]
        public ActionResult ExploreUpload(string nameHack, int? id)
        {
            ViewBag.nameHack = nameHack;
            ViewBag.hackathonId = id.Value;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExploreUpload(HttpPostedFileBase video, ManageExplore model, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!isValidContentType(video.ContentType))
            {
                ViewBag.Error = "Only MP4 & OGG files are allowed.";
                return View("ExploreUpload");
            }
            if (!isValidContentLength(video.ContentLength))
            {
                ViewBag.Error = "Your file is too large. Maximum file size = 100 MB.";
                return View("ExploreUpload");
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
                    string fileName = Path.GetFileName(video.FileName);
                    string urlCombine = urlBlob + "/" + fileName;
    
                        Explore e = new Explore();
                        e.title = model.titleExplore;
                        e.descExplore = model.descExplore;
                        e.video = urlCombine;
                        e.idHackathon = (int)id;

                        db.Explores.Add(e);
                        db.SaveChanges();
                }
            }
            return RedirectToAction("Explore", new { id = id });
        }
        //-----<<<<<AZURE STORAGE---------------------------------------------------------------------------

        // POST: HackathonManage/Explore
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExploreUpload(HttpPostedFileBase video, ManageExplore model, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    if (!isValidContentType(video.ContentType))
        //    {
        //        ViewBag.Error = "Only MP4 & OGG files are allowed.";
        //        return View("ExploreUpload");
        //    }
        //    else if (!isValidContentLength(video.ContentLength))
        //    {
        //        ViewBag.Error = "Your file is too large. Maximum file size = 100 MB.";
        //        return View("ExploreUpload");
        //    }
        //    else
        //    {
        //        if (video.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(video.FileName);
        //            var path = Path.Combine(Server.MapPath("~/Content/Videos/Explore/"), fileName);
        //            video.SaveAs(path);
        //            ViewBag.fileName = video.FileName;
        //            var videoUrl = video.FileName;

        //            if (ModelState.IsValid)
        //            {
        //                Explore e = new Explore();
        //                e.title = model.titleExplore;
        //                e.descExplore = model.descExplore;
        //                e.video = videoUrl;
        //                e.idHackathon = (int)id;

        //                db.Explores.Add(e);
        //                db.SaveChanges();
        //                return RedirectToAction("Explore", new { id = id });
        //            }
        //        }
        //        return View("ExploreUpload", new { id = id });
        //    }
        //}

        // GET: Explore/Edit
        [Authorize]
        public ActionResult ExploreEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Explore explore = db.Explores.Find(id);
            if (explore == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExploreId = explore.idHackathon;
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "nameHack", explore.idHackathon);
            return View(explore);
        }

        // POST: Explore/Edit
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExploreEdit([Bind(Include = "idExplore,title,descExplore,video,idHackathon")] Explore explore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(explore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Explore", new { id = explore.idHackathon });
            }
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "nameHack", explore.idHackathon);
            return View(explore);
        }

        // GET: Explore/Delete
        [Authorize]
        public ActionResult ExploreDelete(int? id, string url)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Explore explore = db.Explores.Find(id);
            if (explore == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExploreId = explore.idHackathon;
            return View(explore);
        }

        // POST: Explore/Delete
        [Authorize]
        [HttpPost, ActionName("ExploreDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ExploreDeleteConfirmed(int id, string url)
        {
            Explore explore = db.Explores.Find(id);
            db.Explores.Remove(explore);
            db.SaveChanges();

            Uri uri = new Uri(url);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
            blob.Delete();

            return RedirectToAction("Explore", new { id = explore.idHackathon});
        }

        [Authorize]
        public ActionResult RedirectSelectHack(int? id)
        {
            return RedirectToAction("SelectHack", "Home", new { id = id });
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

        [Authorize]
        public ActionResult RedirectLaunch(int? id)
        {
            return RedirectToAction("Launch", "Launch", new { id = id });
        }
    }
}
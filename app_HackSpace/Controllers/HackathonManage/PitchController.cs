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
    public class PitchController : Controller
    {
        private HackSpaceDBEntities db = new HackSpaceDBEntities();

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("video/mp4") || contentType.Equals("video/ogg");
        }

        private bool isValidContentLength(int contentLength)
        {
            return (contentLength / 100000000) <= 1; //100000000  octets = 100 Mo
        }

        //-----AZURE STORAGE>>>>>---------------------------------------------------------------------------
        BlobServicesPitch _blobServices = new BlobServicesPitch();
        // GET: HackathonManage/Pitch
        [Authorize]
        public ActionResult Pitch(int? id)
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
            var pitch = db.Pitches.Where(p => p.idHackathon == id.Value);
            viewModel.pitch = pitch;

            return View(viewModel);
        }

        [Authorize]
        public ActionResult PitchUpload(string nameHack, int? id)
        {
            ViewBag.nameHack = nameHack;
            ViewBag.hackathonId = id.Value;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PitchUpload(HttpPostedFileBase video, ManagePitch model, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!isValidContentType(video.ContentType))
            {
                ViewBag.Error = "Only MP4 & OGG files are allowed.";
                return View("PitchUpload");
            }
            if (!isValidContentLength(video.ContentLength))
            {
                ViewBag.Error = "Your file is too large. Maximum file size = 100 MB.";
                return View("PitchUpload");
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

                    Pitch p = new Pitch();
                    p.title = model.titlePitch;
                    p.video = urlCombine;
                    p.idHackathon = (int)id;

                    db.Pitches.Add(p);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Pitch", new { id = id });
        }
        //-----<<<<<AZURE STORAGE---------------------------------------------------------------------------

        // POST: HackathonManage/Pitch
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult PitchUpload(HttpPostedFileBase video, ManagePitch model, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    if (!isValidContentType(video.ContentType))
        //    {
        //        ViewBag.Error = "Only MP4 & OGG files are allowed.";
        //        return View("PitchUpload");
        //    }
        //    else if (!isValidContentLength(video.ContentLength))
        //    {
        //        ViewBag.Error = "Your file is too large. Maximum file size = 100 MB.";
        //        return View("PitchUpload");
        //    }
        //    else
        //    {
        //        if (video.ContentLength > 0)
        //        {
        //            string fileName = Path.GetFileName(video.FileName);
        //            string path = Path.Combine(Server.MapPath("~/Content/Videos/Pitch/"), fileName);
        //            video.SaveAs(path);
        //            ViewBag.fileName = video.FileName;
        //            string videoUrl = video.FileName;

        //            if (ModelState.IsValid)
        //            {
        //                Pitch p = new Pitch();
        //                p.title = model.titlePitch;
        //                p.video = videoUrl;
        //                p.idHackathon = (int)id;

        //                db.Pitches.Add(p);
        //                db.SaveChanges();
        //                return RedirectToAction("Pitch", new { id = id });
        //            }
        //        }
        //        return View("PitchUpload", new { id = id });
        //    }
        //}

        // GET: Pitch/Edit
        [Authorize]
        public ActionResult PitchEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pitch pitch = db.Pitches.Find(id);
            if (pitch == null)
            {
                return HttpNotFound();
            }
            ViewBag.PitchId = pitch.idHackathon;
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "nameHack", pitch.idHackathon);
            return View(pitch);
        }

        // POST: Pitch/Edit
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PitchEdit([Bind(Include = "idPitch,title,video,idHackathon")] Pitch pitch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pitch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Pitch", new { id = pitch.idHackathon });
            }
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "nameHack", pitch.idHackathon);
            return View(pitch);
        }

        // GET: Pitch/Delete
        [Authorize]
        public ActionResult PitchDelete(int? id, string url)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pitch pitch = db.Pitches.Find(id);
            if (pitch == null)
            {
                return HttpNotFound();
            }
            ViewBag.PitchId = pitch.idHackathon;
            return View(pitch);
        }

        // POST: Pitch/Delete
        [Authorize]
        [HttpPost, ActionName("PitchDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PitchDeleteConfirmed(int id, string url)
        {
            Pitch pitch = db.Pitches.Find(id);
            db.Pitches.Remove(pitch);
            db.SaveChanges();

            Uri uri = new Uri(url);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
            blob.Delete();

            return RedirectToAction("Pitch", new { id = pitch.idHackathon });
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

        //[Authorize]
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
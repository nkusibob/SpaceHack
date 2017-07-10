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
    public class DeliverController : Controller
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
        BlobServicesDeliver _blobServices = new BlobServicesDeliver();
        // GET: BlobStorage
        [Authorize]
        public ActionResult Deliver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            //List<string> blobs = new List<string>();

            //foreach (var blobItem in blobContainer.ListBlobs())
            //{
            //    blobs.Add(blobItem.Uri.ToString());
            //}

            Hackathon idHack = db.Hackathons.Where(x => x.idHackathon == id.Value).FirstOrDefault();
            var nameHack = idHack.nameHack;
            ViewBag.nameHack = nameHack;

            ViewBag.hackathonId = id.Value;

            var viewModel = new ManageModel();
            var deliver = db.Delivers.Where(d => d.idHackathon == id.Value);
            viewModel.deliver = deliver;

            return View(viewModel);
        }
        [Authorize]
        public ActionResult DeliverUpload(string nameHack, int? id)
        {
            ViewBag.nameHack = nameHack;
            ViewBag.hackathonId = id.Value;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeliverUpload(HttpPostedFileBase image, ManageDeliver model, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!isValidContentType(image.ContentType))
            {
                ViewBag.Error = "Only JPG, JPEG, PNG & GIF files are allowed.";
                return View("DeliverUpload");
            }
            if (!isValidContentLength(image.ContentLength))
            {
                ViewBag.Error = "Your file is too large. Maximum file size = 3 MB.";
                return View("DeliverUpload");
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

                    Deliver d = new Deliver();
                    d.title = model.titleDeliver;
                    d.descDeliver = model.descDeliver;
                    d.logo = urlCombine;
                    d.idHackathon = (int)id;

                    db.Delivers.Add(d);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Deliver", new {id = id});
        }
        //-----<<<<<AZURE STORAGE---------------------------------------------------------------------------

        // GET: HackathonManage/Deliver
        //[Authorize]
        //public ActionResult Deliver(int? id)
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

        //    var deliver = db.Delivers.Include(d => d.Hackathon).Where(d => d.idHackathon == id.Value);

        //    viewModel.deliver = deliver;

        //    return View(viewModel);
        //}

        //[Authorize]
        //public ActionResult DeliverUpload(string nameHack, int? id)
        //{
        //    ViewBag.nameHack = nameHack;
        //    ViewBag.hackathonId = id.Value;
        //    return View();
        //}

        // POST: HackathonManage/Deliver
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeliverUpload(HttpPostedFileBase image, ManageDeliver model, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    if (!isValidContentType(image.ContentType))
        //    {
        //        ViewBag.Error = "Only JPG, JPEG, PNG & GIF files are allowed.";
        //        return View("DeliverUpload");
        //    }
        //    else if (!isValidContentLength(image.ContentLength))
        //    {
        //        ViewBag.Error = "Your file is too large. Maximum file size = 3 MB.";
        //        return View("DeliverUpload");
        //    }
        //    else
        //    {
        //        if (image.ContentLength > 0)
        //        {
        //            string fileName = Path.GetFileName(image.FileName);
        //            string path = Path.Combine(Server.MapPath("~/Content/Images/Deliver/"), fileName);
        //            image.SaveAs(path);
        //            ViewBag.fileName = image.FileName;
        //            string imgUrl = image.FileName;

        //            if (ModelState.IsValid)
        //            {
        //                Deliver d = new Deliver();
        //                d.title = model.titleDeliver;
        //                d.descDeliver = model.descDeliver;
        //                d.logo = imgUrl;
        //                d.idHackathon = (int)id;

        //                db.Delivers.Add(d);
        //                db.SaveChanges();
        //                return RedirectToAction("Deliver", new { id = id });
        //            }
        //        }
        //    }
        //    return View("DeliverUpload", new { id = id });
        //}

        // GET: Deliver/Edit
        [Authorize]
        public ActionResult DeliverEdit(int? id, string url)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deliver deliver = db.Delivers.Find(id);
            if (deliver == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeliverId = deliver.idHackathon;
            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "nameHack", deliver.idHackathon);
            return View(deliver);
        }

        // POST: Deliver/Edit
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliverEdit([Bind(Include = "idDeliver,title,descDeliver,logo,idHackathon")] Deliver deliver, FormCollection images, string url)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Deliver", new { id = deliver.idHackathon});
            }

            //try
            //{
            //    Uri uri = new Uri(images["ImageName"]);
            //    string filename = System.IO.Path.GetFileName(uri.LocalPath);
            //    CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            //    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
            //    blob.Delete();

            //    foreach (string item in Request.Files)
            //    {
            //        HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
            //        if (file.ContentLength == 0)
            //            continue;

            //        if (file.ContentLength > 0)
            //        {
            //            string nn = file.FileName;
            //            CloudBlobContainer blobContainer1 = _blobServices.GetCloudBlobContainer();
            //            string[] arr = images["ImageName"].Split('/');
            //            CloudBlockBlob blob1 = blobContainer1.GetBlockBlobReference(arr[arr.Length - 1]);
            //            blob1.UploadFromStream(file.InputStream);
            //        }
            //    }
            //    return RedirectToAction("Deliver", new { id = deliver.idHackathon });
            //}
            //catch (Exception)
            //{
            //    return RedirectToAction("DeliverEdit", new { id = deliver.idHackathon, url = url});
            //}

            ViewBag.idHackathon = new SelectList(db.Hackathons, "idHackathon", "nameHack", deliver.idHackathon);
            return View(deliver);
        }

        // GET: Deliver/Delete
        [Authorize]
        public ActionResult DeliverDelete(int? id, string url)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deliver deliver = db.Delivers.Find(id);
            if (deliver == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeliverId = deliver.idHackathon;
            return View(deliver);
        }

        // POST: Deliver/Delete
        [Authorize]
        [HttpPost, ActionName("DeliverDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeliverDeleteConfirmed(int id, string url)
        {
            Deliver deliver = db.Delivers.Find(id);
            db.Delivers.Remove(deliver);
            db.SaveChanges();

            Uri uri = new Uri(url);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
            blob.Delete();

            return RedirectToAction("Deliver", new { id = deliver.idHackathon});
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
        public ActionResult RedirectLaunch(int? id)
        {
            return RedirectToAction("Launch", "Launch", new { id = id });
        }
    }
}
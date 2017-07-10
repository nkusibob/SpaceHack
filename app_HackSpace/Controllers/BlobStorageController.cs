using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app_HackSpace.Controllers
{
    [Authorize]
    public class BlobStorageController : Controller
    {
        BlobServices _blobServices = new BlobServices();
        // GET: BlobStorage
        public ActionResult Upload()
        {
            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                blobs.Add(blobItem.Uri.ToString());
            }
            return View(blobs);
        }

        [HttpPost]
        public ActionResult Upload(FormCollection image)
        {
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
                }
            }
            return RedirectToAction("Upload");
        }

        [HttpPost]
        public string DeleteImg(string Name)
        {
            Uri uri = new Uri(Name);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
            blob.Delete();
            return "File Successfully Deleted";
        }

        //First Action for [httpGet] request:  
        [HttpGet]
        public ActionResult Edit(string Name)
        {
            return PartialView("Edit", Name);
        }

        //Second Action for [HttpPost] request:   
        [HttpPost]
        public ActionResult Edit(FormCollection images)
        {
            try
            {
                Uri uri = new Uri(images["ImageName"]);
                string filename = System.IO.Path.GetFileName(uri.LocalPath);
                CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
                blob.Delete();

                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    if (file.ContentLength == 0)
                        continue;

                    if (file.ContentLength > 0)
                    {
                        string nn = file.FileName;
                        CloudBlobContainer blobContainer1 = _blobServices.GetCloudBlobContainer();
                        string[] arr = images["ImageName"].Split('/');
                        CloudBlockBlob blob1 = blobContainer1.GetBlockBlobReference(arr[arr.Length - 1]);
                        blob1.UploadFromStream(file.InputStream);
                    }
                }
                return RedirectToAction("Upload");
            }
            catch (Exception)
            {
                return PartialView("Edit", images["ImageName"]);
            }
        }
    }
}
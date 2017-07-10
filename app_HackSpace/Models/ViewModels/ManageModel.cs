using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_HackSpace.Models.ViewModels
{
    public class ManageModel
    {
        public IEnumerable<Explore> explore { get; set; }
        public IEnumerable<Pitch> pitch { get; set; }
        public IEnumerable<Deliver> deliver { get; set; }
        public IEnumerable<Launch> launch { get; set; }
    }
}
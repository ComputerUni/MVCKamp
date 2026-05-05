using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        FileImageManager fm = new FileImageManager(new EfImageFileDal());

        public ActionResult Index()
        {
            var files = fm.GetList();
            return View(files);
        }
    }
}
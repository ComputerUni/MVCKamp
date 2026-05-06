using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        // GET: Default

        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());

        public PartialViewResult Index()
        {
            var contentList = cm.GetList();
            return PartialView(contentList);
        }

        public ActionResult Headings()
        {
            var headingList = hm.GetList();
            return View(headingList);
        }
    }
}
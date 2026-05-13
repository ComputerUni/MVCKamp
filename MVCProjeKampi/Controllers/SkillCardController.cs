using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MVCProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class SkillCardController : Controller
    {
        SkillsManager sm = new SkillsManager(new EfSkillsDal());
        CardAboutManager cam = new CardAboutManager(new EfCardAboutDal());  
        public ActionResult Index()
        {
            var cardAboutValues = cam.GetList();
            return View(cardAboutValues);
        }

        public PartialViewResult Skills()
        {
            var skills = sm.GetList();
            return PartialView(skills);
        }
    }
}
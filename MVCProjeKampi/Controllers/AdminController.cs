using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class AdminController : Controller
    {
        AdminManager adm = new AdminManager(new EfAdminDal());
        AdminValidator adminValidator = new AdminValidator();

        public ActionResult Index()
        {
            var adminValues = adm.GetList();
            return View(adminValues);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            List<SelectListItem> arv = (from admin in adm.GetList()
                                                   select admin.AdminRole).Distinct().Select(role => new SelectListItem
                                                   {
                                                       Text = role,
                                                       Value = role
                                                   }).ToList();
            ViewBag.arv = arv;
            return View();
        }


        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            List<SelectListItem> arv = (from admin in adm.GetList()
                                        select admin.AdminRole).Distinct().Select(role => new SelectListItem
                                        {
                                            Text = role,
                                            Value = role
                                        }).ToList();
            ViewBag.arv = arv;
            ValidationResult result = adminValidator.Validate(p);
            if (result.IsValid)
            {
                adm.AdminAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var irem in result.Errors)
                {
                    ModelState.AddModelError(irem.PropertyName, irem.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            List<SelectListItem> adminRoleList = (from admin in adm.GetList()
                                        select admin.AdminRole).Distinct().Select(role => new SelectListItem
                                        {
                                            Text = role,
                                            Value = role
                                        }).ToList();
            ViewBag.adminRoleList = adminRoleList;
            var adminValues = adm.GetByID(id);
            return View(adminValues);
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            List<SelectListItem> adminRoleList = (from admin in adm.GetList()
                                        select admin.AdminRole).Distinct().Select(role => new SelectListItem
                                        {
                                            Text = role,
                                            Value = role
                                        }).ToList();
            ViewBag.adminRoleList = adminRoleList;
            adm.AdminUpdate(p);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAdmin(int id)
        {
            var adminValue = adm.GetByID(id);
            adminValue.AdminStatus = false;
            adm.AdminDelete(adminValue);
            return RedirectToAction("Index");
        }

        //public ActionResult CreateAdmin()
        //{
        //    Admin admin = new Admin();
        //    admin.AdminUserName = "irem";
        //    admin.AdminPassword = "123456";
        //    admin.AdminRole = "B";

        //    adm.AdminAdd(admin);


        //    return Content("Admin başarıyla veritabanına kaydedildi!");
        //}
    }
}

using Crud2.DB_Context;
using Crud2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Crud2.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            detailEntities1 Db = new detailEntities1();
            var Data = Db.students.ToList();
            return View(Data);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            detailEntities1 Db = new detailEntities1();
            var Data = Db.students.Where(m => m.id == id).First();
            Db.students.Remove(Data);
            Db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult Logpage( )
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logpage(User modelob)
        {
            detailEntities1 ent = new detailEntities1();

            var res = ent.usrs.Where(m => m.usrname == modelob.usrname).FirstOrDefault();
            if (res == null)
            {
                TempData["invalid"] = "Invalid Email";
            }
            else
            {
                if(res.usrname==modelob.usrname && res.pass==modelob.pass)
                {
                    FormsAuthentication.SetAuthCookie(res.usrname, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["worng"] = "Invalid Password";
                }
                
            }
;
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }




        [HttpPost]
        public ActionResult About(EmployeeModel stu)
        {
            detailEntities1 Db = new detailEntities1();

            student tab = new student();

            tab.id = stu.Id;
            tab.Name = stu.Name;
            tab.Address = stu.Address;
            tab.Email = stu.Email;


            if (stu.Id == 0)
            {
                Db.students.Add(tab);
                Db.SaveChanges();
            }
            else
            {
                Db.Entry(tab).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Edit(int id)
        {
            EmployeeModel stu = new EmployeeModel();
            detailEntities1 Db = new detailEntities1();
            var res = Db.students.Where(m => m.id == id).First();

            stu.Id = res.id;
            stu.Name = res.Name;
            stu.Address = res.Address;
            stu.Email = res.Email;

            return View("About",stu);
        }


    }

    //internal class studentModel
    //{
    //    internal string Address;
    //    internal string Email;
    //    internal int Name;

    //    public int id { get; internal set; }
    //}
}
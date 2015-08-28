using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.DAL;
using WebApp.Models;

namespace WebApp.Controllers {

    [Authorize(Roles=("Admin"))]
    public class StudentController : Controller {

        private WebAppDbContext db = new WebAppDbContext();

        // GET: Student/
        public ActionResult Index() {
            return View(db.Students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if(student == null) {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            User user = db.Users.Find(student.UserId);

            if(student == null) {
                return HttpNotFound();
            }
            var service = new StudentService(HttpContext.GetOwinContext().Get<WebAppDbContext>());
            var model = service.MapStudent(user);
            var gender = new List<Gender> {
                new Gender { Sex = "Male", Value = "Male" },
                new Gender { Sex = "Female", Value = "Female"}
            };

            ViewBag.Sex = new SelectList(gender, "Sex", "Value",  model.Sex);

            return View(model);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditStudentViewModel model) {

            if(ModelState.IsValid) {
                var service = new StudentService(HttpContext.GetOwinContext().Get<WebAppDbContext>());
                service.EditStudent(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }


        // GET: Student/Delete/5
        public ActionResult Delete(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if(student == null) {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Student student = db.Students.Find(id);
            
            var user = db.Users.Find(student.UserId);

            db.Users.Remove(user);
            db.Students.Remove(student);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

    }
}

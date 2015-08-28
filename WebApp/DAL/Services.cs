using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DAL {

    public class StudentService {

        private WebAppDbContext db;

        public StudentService(WebAppDbContext dbContext) {
            db = dbContext;
        }

        public void AddStudent(string userId) {

            var student = new Student { UserId = userId };
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void EditStudent(EditStudentViewModel model) {
            var user = db.Users.Find(model.UserId);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Sex = model.Sex;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.Phone;

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public EditStudentViewModel MapStudent(User user) {

            var model = new EditStudentViewModel {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex,
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            return model;
        }
    }


}

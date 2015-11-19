using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models {
    public class Student {

        public Student() {
            TestScores = new HashSet<TestScore>();
        }

        public int StudentId { get; set; }

        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<TestScore> TestScores { get; set; }
    }

    public class EditStudentViewModel {

        public string UserId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public string Sex { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }

    public class Gender {
        public string Sex { get; set; }
        public string Value { get; set; }
    }
}

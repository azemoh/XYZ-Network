using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models {

    public class Test {

        public Test() {
            TestScores = new HashSet<TestScore>();
        }

        public int TestId { get; set; }

        public string Title { get; set; }

        //public string Description { get; set; }

        public virtual List<TestQuestion> Questions { get; set; }

        public virtual ICollection<TestScore> TestScores { get; set; }

    }

    public class TestScore {

        public int Id { get; set; }

        //[Display(Name="Start Time")]
        //public DateTime? StartTime { get; set; }

        //public TimeSpan? Duration { get; set; }

        //[Display(Name = "End Time")]
        //public DateTime? EndTime { get; set; }

        public int Score { get; set; }

        public int TestId { get; set; }

        public int StudentId { get; set; }

        [JsonIgnore]
        public virtual Test Test { get; set; }

        [JsonIgnore]
        public virtual Student Student { get; set; }
    }

    public class TestQuestion {

        public int Id { get; set; }

        public string Code { get; set; }

        [Required]
        public string Question { get; set; }

        public virtual List<TestOption> Options { get; set; }

        public int TestId { get; set; }
        [JsonIgnore]
        public virtual Test Test { get; set; }
    }

    public class TestOption {

        public int Id { get; set; }

        [Required]
        public string Option { get; set; }

        public int QuestionId { get; set; }

        [JsonIgnore]
        public virtual TestQuestion Question { get; set; }

        //[JsonIgnore]
        public bool IsCorrect { get; set; }
    }

}

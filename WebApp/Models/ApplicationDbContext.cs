using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebApp.Models;

namespace WebApp.Models {

    public class ApplicationDbContext : IdentityDbContext<User> {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestScore> TestScores { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<TestOption> TestOptions { get; set; }
        //public virtual DbSet<TestAnswer> TestAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");
        }
    }
}

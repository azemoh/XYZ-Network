using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebApp.Migrations;

namespace WebApp.Models {

  public class WebAppDbContext : IdentityDbContext<User> {

    public WebAppDbContext()
        : base("WebAppContext", throwIfV1Schema: false) {
    }

    public static WebAppDbContext Create() {
      return new WebAppDbContext();
    }

    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Test> Tests { get; set; }
    public virtual DbSet<TestScore> TestScores { get; set; }
    public virtual DbSet<TestQuestion> TestQuestions { get; set; }
    public virtual DbSet<TestOption> TestOptions { get; set; }

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
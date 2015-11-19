namespace WebApp.Migrations {

  using System.Data.Entity.Migrations;

  public partial class dbMigration : DbMigration {

    public override void Up() {
      CreateTable(
          "dbo.Role",
          c => new {
            RoleId = c.String(nullable: false, maxLength: 128),
            Name = c.String(nullable: false, maxLength: 256),
          })
          .PrimaryKey(t => t.RoleId)
          .Index(t => t.Name, unique: true, name: "RoleNameIndex");

      CreateTable(
          "dbo.UserRole",
          c => new {
            UserId = c.String(nullable: false, maxLength: 128),
            RoleId = c.String(nullable: false, maxLength: 128),
          })
          .PrimaryKey(t => new { t.UserId, t.RoleId })
          .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
          .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
          .Index(t => t.UserId)
          .Index(t => t.RoleId);

      CreateTable(
          "dbo.Students",
          c => new {
            StudentId = c.Int(nullable: false, identity: true),
            UserId = c.String(nullable: false, maxLength: 128),
          })
          .PrimaryKey(t => t.StudentId)
          .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.TestScores",
          c => new {
            Id = c.Int(nullable: false, identity: true),
            Score = c.Int(nullable: false),
            TestId = c.Int(nullable: false),
            StudentId = c.Int(nullable: false),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
          .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
          .Index(t => t.TestId)
          .Index(t => t.StudentId);

      CreateTable(
          "dbo.Tests",
          c => new {
            TestId = c.Int(nullable: false, identity: true),
            Title = c.String(),
          })
          .PrimaryKey(t => t.TestId);

      CreateTable(
          "dbo.TestQuestions",
          c => new {
            Id = c.Int(nullable: false, identity: true),
            Code = c.String(),
            Question = c.String(nullable: false),
            TestId = c.Int(nullable: false),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
          .Index(t => t.TestId);

      CreateTable(
          "dbo.TestOptions",
          c => new {
            Id = c.Int(nullable: false, identity: true),
            Option = c.String(nullable: false),
            QuestionId = c.Int(nullable: false),
            IsCorrect = c.Boolean(nullable: false),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.TestQuestions", t => t.QuestionId, cascadeDelete: true)
          .Index(t => t.QuestionId);

      CreateTable(
          "dbo.User",
          c => new {
            UserId = c.String(nullable: false, maxLength: 128),
            FirstName = c.String(maxLength: 50),
            LastName = c.String(maxLength: 50),
            Sex = c.String(),
            Photo = c.Binary(),
            AccountType = c.String(),
            Email = c.String(maxLength: 256),
            EmailConfirmed = c.Boolean(nullable: false),
            PasswordHash = c.String(),
            SecurityStamp = c.String(),
            PhoneNumber = c.String(),
            PhoneNumberConfirmed = c.Boolean(nullable: false),
            TwoFactorEnabled = c.Boolean(nullable: false),
            LockoutEndDateUtc = c.DateTime(),
            LockoutEnabled = c.Boolean(nullable: false),
            AccessFailedCount = c.Int(nullable: false),
            UserName = c.String(nullable: false, maxLength: 256),
          })
          .PrimaryKey(t => t.UserId)
          .Index(t => t.UserName, unique: true, name: "UserNameIndex");

      CreateTable(
          "dbo.UserClaim",
          c => new {
            UserClaimId = c.Int(nullable: false, identity: true),
            UserId = c.String(nullable: false, maxLength: 128),
            ClaimType = c.String(),
            ClaimValue = c.String(),
          })
          .PrimaryKey(t => t.UserClaimId)
          .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.UserLogin",
          c => new {
            LoginProvider = c.String(nullable: false, maxLength: 128),
            ProviderKey = c.String(nullable: false, maxLength: 128),
            UserId = c.String(nullable: false, maxLength: 128),
          })
          .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
          .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
          .Index(t => t.UserId);
    }

    public override void Down() {
      DropForeignKey("dbo.Students", "UserId", "dbo.User");
      DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
      DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
      DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
      DropForeignKey("dbo.TestScores", "TestId", "dbo.Tests");
      DropForeignKey("dbo.TestQuestions", "TestId", "dbo.Tests");
      DropForeignKey("dbo.TestOptions", "QuestionId", "dbo.TestQuestions");
      DropForeignKey("dbo.TestScores", "StudentId", "dbo.Students");
      DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
      DropIndex("dbo.UserLogin", new[] { "UserId" });
      DropIndex("dbo.UserClaim", new[] { "UserId" });
      DropIndex("dbo.User", "UserNameIndex");
      DropIndex("dbo.TestOptions", new[] { "QuestionId" });
      DropIndex("dbo.TestQuestions", new[] { "TestId" });
      DropIndex("dbo.TestScores", new[] { "StudentId" });
      DropIndex("dbo.TestScores", new[] { "TestId" });
      DropIndex("dbo.Students", new[] { "UserId" });
      DropIndex("dbo.UserRole", new[] { "RoleId" });
      DropIndex("dbo.UserRole", new[] { "UserId" });
      DropIndex("dbo.Role", "RoleNameIndex");
      DropTable("dbo.UserLogin");
      DropTable("dbo.UserClaim");
      DropTable("dbo.User");
      DropTable("dbo.TestOptions");
      DropTable("dbo.TestQuestions");
      DropTable("dbo.Tests");
      DropTable("dbo.TestScores");
      DropTable("dbo.Students");
      DropTable("dbo.UserRole");
      DropTable("dbo.Role");
    }
  }
}
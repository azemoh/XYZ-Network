using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApp.Models;

namespace WebApp.Controllers {

    public class TestsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tests
        public async Task<ActionResult> Index() {
            return View(await db.Tests.ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<ActionResult> Details(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = await db.Tests.FindAsync(id);
            if(test == null) {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: Tests/Edit/5
        public async Task<ActionResult> Edit(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = await db.Tests.FindAsync(id);
            if(test == null) {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TestId,Title")] Test test) {
            if(ModelState.IsValid) {
                db.Entry(test).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(test);
        }

        // GET: Tests/Scores/5
        public async Task<ActionResult> Scores() {
            return View(await db.TestScores.ToListAsync());
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class TakeTestController : Controller {

        ApplicationDbContext db = new ApplicationDbContext();

        // GET: TakeTest
        [Authorize(Roles="Student")]
        public ActionResult Index() {

            var userId = User.Identity.GetUserId();
            ViewBag.userId = db.Students.Where(s => s.UserId == userId).First().StudentId;

            return View();
        }
    }
}

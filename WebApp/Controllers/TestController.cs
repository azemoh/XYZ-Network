using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;

namespace WebApp.Controllers {

    [Authorize]
    public class TestController : ApiController {
        private WebAppDbContext db = new WebAppDbContext();

        // GET: api/Tests
        //public IQueryable<Test> GetTests() {
        public HttpResponseMessage GetTests(HttpRequestMessage request) {
            return request.CreateResponse<Test[]>(HttpStatusCode.OK, db.Tests.ToArray());
        }

        // GET: api/Tests/5
        [ResponseType(typeof(Test))]
        public async Task<IHttpActionResult> GetTest(int id) {
            Test test = await db.Tests.FindAsync(id);
            if(test == null) {
                return NotFound();
            }

            return Ok(test);
        }

        // PUT: api/Tests/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTest(int id, Test test) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if(id != test.TestId) {
                return BadRequest();
            }

            db.Entry(test).State = EntityState.Modified;

            try {
                await db.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!TestExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tests
        [ResponseType(typeof(Test))]
        public async Task<IHttpActionResult> PostTest(Test test) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.Tests.Add(test);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = test.TestId }, test);
        }

        // DELETE: api/Tests/5
        [ResponseType(typeof(Test))]
        public async Task<IHttpActionResult> DeleteTest(int id) {
            Test test = await db.Tests.FindAsync(id);
            if(test == null) {
                return NotFound();
            }

            db.Tests.Remove(test);
            await db.SaveChangesAsync();

            return Ok(test);
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestExists(int id) {
            return db.Tests.Count(e => e.TestId == id) > 0;
        }
    }


    public class TestScoreController : ApiController {
        private WebAppDbContext db = new WebAppDbContext();

        //GET: api/TestScore
        [ResponseType(typeof(TestScore))]
        public IHttpActionResult GetTestScores() {

            var testSores = db.TestScores;

            if(testSores == null) {
                return NotFound();
            }

            return Ok(testSores);
        }

        // GET: api/TestScores/5
        [ResponseType(typeof(Test))]
        public async Task<IHttpActionResult> GetTestScore(int id) {
            TestScore testScore = await db.TestScores.FindAsync(id);
            if(testScore == null) {
                return NotFound();
            }

            return Ok(testScore);
        }

        // PUT: api/TestScores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTestScore(int id, TestScore testScore) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if(id != testScore.Id) {
                return BadRequest();
            }

            db.Entry(testScore).State = EntityState.Modified;

            try {
                await db.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!TestScoreExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TestScores
        [ResponseType(typeof(Test))]
        public async Task<IHttpActionResult> PostTestScore(TestScore testScore) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.TestScores.Add(testScore);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = testScore.Id }, testScore);
        }

        private bool TestScoreExists(int id) {
            return db.TestScores.Count(e => e.TestId == id) > 0;
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

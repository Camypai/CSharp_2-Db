using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
//using System.AspNet.OData;
using Web_db.Models;

namespace Web_db.Controllers
{
    public class DepartmentsController : ApiController
    {
        JobContext _db = new JobContext();
        
//        [Queryable]
        public IQueryable<Department> Get()
        {
            return _db.Departments;
        }

//        protected Department Get(int key)
//        {
//            return _db.Departments.Find(key);
//        }

//        public HttpResponseMessage Post(Department entity)
//        {
//            if (!ModelState.IsValid)
//            {
//                return new HttpResponseMessage(HttpStatusCode.BadRequest);
//            }
//            _db.Departments.Add(entity);
//            _db.SaveChanges();
//            return new HttpResponseMessage(HttpStatusCode.Created);
//        }
//
//        public HttpResponseMessage Patch(int id, Delta<Department> patch)
//        {
////            if (!ModelState.IsValid)
////            {
////                return new HttpResponseMessage(HttpStatusCode.BadRequest);
////            }
//            var entity = _db.Departments.Find(id);
//            if (entity == null)
//            {
//                return new HttpResponseMessage(HttpStatusCode.NotFound);
//            }
//            patch.Patch(entity);
//            try
//            {
//                _db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
////                if (!DepartmentsExists(key))
////                {
////                    return new HttpResponseMessage(HttpStatusCode.NotFound);
////                }
//
//                throw;
//            }
//
//            return new HttpResponseMessage(HttpStatusCode.OK);
//        }
//
//        public void Delete(int id)
//        {
//            var department = _db.Departments.Find(id);
//            if (department == null)
//            {
//                return;
//            }
//            _db.Departments.Remove(department);
//            _db.SaveChanges();
//        }
        [Microsoft.AspNet.OData.EnableQuery]
        public SingleResult<Department> Get([Microsoft.AspNet.OData.FromODataUri] int key)
        {
            var result = _db.Departments.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
//
        public IHttpActionResult Post(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Departments.Add(department);
             _db.SaveChanges();
            return Ok(department);
        }
        
        public IHttpActionResult Patch([Microsoft.AspNet.OData.FromODataUri] int key, Microsoft.AspNet.OData.Delta<Department> department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity =  _db.Departments.Find(key);
            if (entity == null)
            {
                return NotFound();
            }
            department.Patch(entity);
            try
            {
                 _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(entity);
        }
        
        public IHttpActionResult Delete([Microsoft.AspNet.OData.FromODataUri] int key)
        {
            var department = _db.Departments.Find(key);
            if (department == null)
            {
                return NotFound();
            }
            _db.Departments.Remove(department);
            _db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool DepartmentsExists(int key)
        {
            return _db.Departments.Any(q => q.Id == key);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
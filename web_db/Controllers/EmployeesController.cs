using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Web_db.Models;

namespace Web_db.Controllers
{
    public class EmployeesController : ApiController
    {
        JobContext _db = new JobContext();
        
//        [Queryable]
        public IQueryable<Employee> Get()
        {
            return _db.Employees;
        }
        
        [EnableQuery]
        public SingleResult<Employee> Get([FromODataUri] int key)
        {
            var result = _db.Employees.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return Ok(employee);
        }
        
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Employee> employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _db.Employees.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            employee.Patch(entity);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(employee);
        }
        
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var employee = await _db.Employees.FindAsync(key);
            if (employee == null)
            {
                return NotFound();
            }
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool EmployeeExists(int key)
        {
            return _db.Employees.Any(q => q.Id == key);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
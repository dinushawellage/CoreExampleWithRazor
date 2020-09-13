using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEmpManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreEmpManagement.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmpController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmpController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data=await _db.Employees.ToListAsync() });
        }

        [HttpDelete]
        public async  Task<IActionResult> Delete(int id)
        {
            var emp =  _db.Employees.FirstOrDefault(t => t.ID==id);
            if(emp==null)
            {
                return Json(new {success=false, message="Error while deleting" });
            }
            _db.Employees.Remove(emp);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEmpManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreEmpManagement.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Employee> Employees { get; set; }
        public async Task OnGet()
        {
            Employees = await _db.Employees.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int Id)
        {
            var emp = await _db.Employees.FindAsync(Id);
            if (emp == null)
            {
                return NotFound();
            }
            _db.Employees.Remove(emp);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}

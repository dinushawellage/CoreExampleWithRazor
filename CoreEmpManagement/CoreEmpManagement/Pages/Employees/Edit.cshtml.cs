using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using CoreEmpManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreEmpManagement.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public async Task OnGet(int Id)
        {
            Employee = await _db.Employees.FindAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var empFromDB= await _db.Employees.FindAsync(Employee.ID);
                empFromDB.Name = Employee.Name;
                empFromDB.Age = Employee.Age;
                empFromDB.Location = Employee.Location;
                empFromDB.Salary = Employee.Salary;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
                return Page();
        }
    }
}

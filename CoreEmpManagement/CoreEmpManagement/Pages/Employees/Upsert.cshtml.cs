using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEmpManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreEmpManagement.Pages.Employees
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public async Task<IActionResult> OnGet(int? Id)
        {
            if(Id==null || Id==0)
            {
                Employee = new Employee();
                return Page();
            }
            Employee = await _db.Employees.FindAsync(Id);
            if(Employee==null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Employee.ID==0)
                {
                     _db.Add(Employee);
                }
                else
                {
                     _db.Update(Employee);

                }
               
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
                return Page();
        }
    }
}

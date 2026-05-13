using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity1.Data;
using ContosoUniversity1.Models;

namespace ContosoUniversity1.Pages_Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity1.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity1.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}

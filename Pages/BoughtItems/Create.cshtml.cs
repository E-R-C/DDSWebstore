using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.BoughtItems
{
    public class CreateModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;

        public CreateModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BoughtItem BoughtItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BoughtItem.Add(BoughtItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
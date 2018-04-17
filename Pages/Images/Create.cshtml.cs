using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.Images
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
        ViewData["ItemID"] = new SelectList(_context.Item, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Image Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Image.Add(Image);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
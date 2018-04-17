using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.BoughtItems
{
    public class EditModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;

        public EditModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BoughtItem BoughtItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BoughtItem = await _context.BoughtItem.SingleOrDefaultAsync(m => m.ID == id);

            if (BoughtItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BoughtItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoughtItemExists(BoughtItem.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BoughtItemExists(int id)
        {
            return _context.BoughtItem.Any(e => e.ID == id);
        }
    }
}

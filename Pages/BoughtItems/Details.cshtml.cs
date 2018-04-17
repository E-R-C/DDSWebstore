using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.BoughtItems
{
    public class DetailsModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;

        public DetailsModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

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
    }
}

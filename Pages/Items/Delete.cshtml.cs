using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DeleteModel(DDSWebstore.Models.MyDBContext context, IHostingEnvironment hostingEnvironment)
        {
             _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [BindProperty]
        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Item.SingleOrDefaultAsync(m => m.ID == id);

            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Item.FindAsync(id);

            if (Item != null)
            {   
                // var fileName = this.Item.ImageName;
                // var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                // var filePath = Path.Combine(uploads, fileName);
                // _context.Item.Remove(Item);
                // await _context.SaveChangesAsync();

                // if (System.IO.File.Exists(filePath))
                // {
                //     System.IO.File.Delete(filePath);
                // }
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.Items
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
        public Item Item { get; set; }
        [BindProperty]
        public Image Image {get; set;}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Item.Add(Item);
            Image.ItemID = Item.ID;
            _context.Image.Add(Image);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
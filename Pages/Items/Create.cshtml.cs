using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CreateModel(DDSWebstore.Models.MyDBContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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

        [BindProperty]
        public IFormFile Image {set; get;}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ArrayList images = new ArrayList();
            if (this.Image != null) 
            {
                var fileName = this.Image.FileName;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, fileName);
                this.Image.CopyTo(new FileStream (filePath, FileMode.Create));
                this.Item.ImageName = fileName;
                images.Add(new Image{ImageURL=filePath});
            }           

            _context.Item.Add(Item);
            foreach(Image i in images) {
                i.ItemID = Item.ID;
                _context.Image.Add(i);

            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
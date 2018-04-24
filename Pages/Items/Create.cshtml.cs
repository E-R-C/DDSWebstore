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
        public List <IFormFile> Image {set; get;}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ArrayList images = new ArrayList();
            if (this.Image != null) 
            {
                foreach (IFormFile f in Image) {
                var fileName = f.FileName;
                // there needs to be validation on filename
                String uploadFolder = "uploads";
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, uploadFolder);
                var filePath = Path.Combine(uploads, fileName);
                f.CopyTo(new FileStream (filePath, FileMode.Create));
                var filePath2 = Path.Combine(uploadFolder, fileName);
                images.Add(new Image{ImageURL=filePath2});
                }
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EditModel(DDSWebstore.Models.MyDBContext context, IHostingEnvironment hostingEnvironment)
        {
             _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [BindProperty]
        public Item Item { get; set; }

        [BindProperty]
        public List <IFormFile> AddImage {set; get;}

        //public string[] urlArray {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Item.FindAsync(id);
            if (Item == null)
            {
                return NotFound();
            }
           // urlArray = Directory.GetFiles(Path.Combine(_hostingEnvironment.WebRootPath, 
           //     Path.Combine("uploads", "ImageFolder_" + Item.FID)));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var itemToUpdate = await _context.Item.FindAsync(id);
            ArrayList images = new ArrayList();
            if (this.AddImage != null) 
            {
                String uploadFolder = "uploads";
                String newDir = "ImageFolder_" + itemToUpdate.FID;
                var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, uploadFolder);
                var newDirPath = Path.Combine(uploadPath, newDir);
                foreach (IFormFile f in AddImage) {
                    var fileName = f.FileName;
                    // there needs to be validation on filename
                    var filePath = Path.Combine(newDirPath, fileName);
                    FileStream stream = new FileStream (filePath, FileMode.Create);
                    f.CopyTo(stream);
                    stream.Close();
                    var filePath2 = Path.Combine(Path.Combine(uploadFolder, newDir), fileName);
                    images.Add(new Image{ImageURL=filePath2});
                }
            }
            foreach(Image i in images) {
                i.ItemID = Item.ID;
                _context.Image.Add(i);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.ID))
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

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ID == id);
        }
    }
}

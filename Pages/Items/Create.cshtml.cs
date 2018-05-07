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

        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
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
            Item.FID = generateID();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ArrayList images = new ArrayList();
            if (this.Image != null) 
            {
                String uploadFolder = "uploads";
                String newDir = "ImageFolder_" + Item.FID;
                var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, uploadFolder);
                var newDirPath = Path.Combine(uploadPath, newDir);
                if (!Directory.Exists(newDirPath)) 
                {
                    Directory.CreateDirectory(newDirPath);
                }
                foreach (IFormFile f in Image) {
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
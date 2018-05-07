using System;
using System.Web;
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

        [BindProperty]
        public string[] urlArray {get; set;}

        [BindProperty]
        public string IndexText {get; set;}

        [BindProperty]
        public string mainImageIndex {get; set;}

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
            urlArray = Directory.GetFiles(Path.Combine(_hostingEnvironment.WebRootPath, 
                Path.Combine("uploads", "ImageFolder_" + Item.FID)));
            for (int i = 0; i < urlArray.Length; i++) 
            {
                urlArray[i] = urlArray[i].Replace(_hostingEnvironment.WebRootPath, "");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var itemToUpdate = await _context.Item.FindAsync(id);
            _context.Attach(Item).State = EntityState.Modified;
            
            // Delete Image
            if (IndexText != null) 
            {
                urlArray = Directory.GetFiles(Path.Combine(_hostingEnvironment.WebRootPath, 
                    Path.Combine("uploads", "ImageFolder_" + Item.FID)));
                List<string> indexList = IndexText.Split(",").ToList();
                DbSet<Image> DbSetCopy = _context.Image;
                if (indexList.Count != 0)
                {
                    foreach (var entity in DbSetCopy)
                    {

                        for (int i = 0; i < indexList.Count - 1; i++)
                        {   
                            string url = urlArray[int.Parse(indexList[i])];
                            if (Path.GetFileName(entity.ImageURL).Equals(Path.GetFileName(url)))
                            {
                               _context.Image.Remove(entity);
                               FileInfo f = new FileInfo(@url);
                               f.Delete();

                            }
                        }
                    }
                }   
            }

            // Add Image
            ArrayList images = new ArrayList();
            if (this.AddImage != null) 
            {
                String uploadFolder = "uploads";
                String newDir = "ImageFolder_" + Item.FID;
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

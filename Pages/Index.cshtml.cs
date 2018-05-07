using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DDSWebstore.Models;

namespace DDSWebstore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;

        public IndexModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        public string Search {get; set;}

        public IList<Item> Item { get;set; }
        public IList<DDSWebstore.Models.Image> Image { get;set; }

        public IList<string> Tags {get;set;}

        public async Task OnGetAsync(string search)
        {
            var items = from i in _context.Item
                        select i;

            if (!String.IsNullOrEmpty(search))
            {
                if (search.Equals("All")) {
                     items = from i in _context.Item select i;
                }
                else {
                    string l_search = search.ToLower();
                    if (search.Length > 1) {
                        items = items.Where(s => s.Name.ToLower().Contains(l_search) || s.Description.ToLower().Contains(l_search) 
                            || s.Tags.Contains(search.First().ToString().ToUpper() + search.Substring(1)));
                    } else {
                        items = items.Where(s => s.Name.ToLower().Contains(l_search) || s.Description.ToLower().Contains(l_search) 
                            || s.Tags.Contains(search.First().ToString().ToUpper()));
                    }
                }
                
            }
            
            Search = search;
            

            Item = await items.Include(i => i.Images).ToListAsync();
            
            HashSet<String> tags = new HashSet<String>();
            foreach (var i in _context.Item) {
                if (i.Tags != "") {
                    string[] splitTags = i.Tags.Split(',');
                    foreach (string tag in splitTags) {
                        tags.Add(tag);
                    }
                }
            }
            List<string> sorted = tags.ToList();
            sorted.Sort();
            Console.WriteLine(sorted.ToString());
            if (!sorted.Contains("All")) {
                sorted.Insert(0, "All");
            }
            Tags = sorted;

        }
    }
}

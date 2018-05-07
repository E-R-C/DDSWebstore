﻿using System;
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

        public IList<Item> Item { get;set; }
        public IList<DDSWebstore.Models.Image> Image { get;set; }

        public IList<string> Tags {get;set;}

        public async Task OnGetAsync(string search)
        {
            var items = from i in _context.Item
                        select i;

            if (!String.IsNullOrEmpty(search))
            {
                items = items.Where(s => s.Name.Contains(search) || s.Description.Contains(search));
            }

            Item = await items.Include(i => i.Images).ToListAsync();
            
            HashSet<String> tags = new HashSet<String>();
            foreach (var i in _context.Item) {
                string[] splitTags = i.Tags.Split(',');
                foreach (string tag in splitTags) {
                    tags.Add(tag);
                }
            }
            List<string> sorted = tags.ToList();
            sorted.Sort();


            Tags = sorted;

        }
    }
}

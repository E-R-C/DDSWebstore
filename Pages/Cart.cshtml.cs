using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DDSWebstore.Models;
using Microsoft.AspNetCore.Http;

namespace DDSWebstore.Pages
{
    public class CartModel : PageModel{
        private readonly DDSWebstore.Models.MyDBContext _context;
        public List<string> cookieResults;

        public CartModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }
        public async Task OnGetAsync()
        { 
            string ddsCookie = Request.Cookies["ddsCookie"];
            this.cookieResults = ddsCookie.Split(',').ToList();
            var  items = _context.Item.Where(t => cookieResults.Contains(t.ID.ToString()));
            // var items = _context.Item.Join(cookieResults, up => up.ID, id => id, (up, id) => up);
            
            Item = await items.ToListAsync();
        }
    }
}
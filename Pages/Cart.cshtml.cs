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
    public class CartModel : PageModel{
        private readonly DDSWebstore.Models.MyDBContext _context;

        public CartModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }
        public async Task OnGetAsync()
        {
            Item = await _context.Item.ToListAsync();
        }
    }
}
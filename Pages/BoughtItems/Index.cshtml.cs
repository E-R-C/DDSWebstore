using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DDSWebstore.Models;

namespace DDSWebstore.Pages.BoughtItems
{
    public class IndexModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;

        public IndexModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        public IList<BoughtItem> BoughtItem { get;set; }

        public async Task OnGetAsync()
        {
            BoughtItem = await _context.BoughtItem.ToListAsync();
        }
    }
}

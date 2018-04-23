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
        // private CookieOptions ddsCookie;

        public CartModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }
        public async Task OnGetAsync()
        {   
            Item = await _context.Item.ToListAsync();
            cookieContents = Request.Cookies["ddsCookie"];
            IList<Item> returned = new List<Item>();
            foreach(int id in cookieContents){
                var item = _context.Item.Single(i => i.ID == id);
                returned.Add(item);
            }
            Item = await returned.ToListAsync();

    }
}


// using System.Linq;
// using ViewInjectSample.Interfaces;

// namespace ViewInjectSample.Model.Services
// {
//     public class StatisticsService
//     {
//         private readonly IToDoItemRepository _toDoItemRepository;

//         public StatisticsService(IToDoItemRepository toDoItemRepository)
//         {
//             _toDoItemRepository = toDoItemRepository;
//         }

//         public int GetCount()
//         {
//             return _toDoItemRepository.List().Count();
//         }

//         public int GetCompletedCount()
//         {
//             return _toDoItemRepository.List().Count(x => x.IsDone);
//         }

//         public double GetAveragePriority()
//         {
//             if (_toDoItemRepository.List().Count() == 0)
//             {
//                 return 0.0;
//             }

//             return _toDoItemRepository.List().Average(x => x.Priority);
//         }
//     }
// }
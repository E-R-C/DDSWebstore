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
        public List<int> cookieResults;
        public static string ddsCookie;

        public CartModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }
        public async Task OnGetAsync()
        { 
            CartModel.ddsCookie = Request.Cookies["ddsCookie"];
            // foreach(var i in ddsCookie.Split(',').ToList()){
            //     this.cookieResults.Add(int.Parse(i));
            // }
            Console.Write(CartModel.ddsCookie);
            // foreach(var c in this.ddsCookie){
            //     Console.
            // }
            if(CartModel.ddsCookie != null){
                this.cookieResults = this.parseCookieResults(CartModel.ddsCookie);//ddsCookie.Split(',').ToList();
                Console.Write(" ");
                Console.Write(this.cookieResults);
                Console.Write(" ");
                var  items = _context.Item.Where(t => cookieResults.Contains(t.ID));
                // var items = _context.Item.Join(cookieResults, up => up.ID, id => id, (up, id) => up);
                
                Item = await items.ToListAsync();
            }else{
                Item = new List<Item>();
            }
        }

        private List<int> parseCookieResults(string results){

            List<int> toReturn = new List<int>();
            foreach ( var c in results) {
                Console.Write(c + " ");
                if(c != '[' && c != ']' && c != ','){
                    toReturn.Add(int.Parse(c.ToString()));
                }
            }
            return toReturn;
        }
    }
}
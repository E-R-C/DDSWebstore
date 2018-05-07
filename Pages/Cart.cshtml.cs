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
                
                Item = await items.Include(s => s.Images).ToListAsync();
            }else{
                Item = new List<Item>();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> OnPostAsync (string name, string address,
         string city, string state, int zip){
            ddsCookie = Request.Cookies["ddsCookie"];
            Console.WriteLine(" INSIDE ASYNC TASK POST");
            var items = _context.Item.Where(t => cookieResults.Contains(t.ID));
            
            float totPrice = 0;
            foreach (Item i in items){
                totPrice += i.Price;
            }
            Order order = new Order{
                Name = name,
                StreetAddress = address,
                City = city,
                State = state,
                Zipcode = zip,
                Price = (float) Math.Round(totPrice,2)
            };
            _context.Order.Add(order);
            foreach (Item i in items){
                _context.BoughtItem.Add(convertItem(i,order.ID));
                _context.Item.Remove(i);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./OrderConfirmation");
         }
        private List<int> parseCookieResults(string results){
            results = results.Trim().Trim(']').Trim('[');
            // results = results.Substring(1, results.Length - 2);
            Console.WriteLine(results);
            string[] nums = results.Split(",");
            Console.Write(nums);

            List<int> toReturn = new List<int>();
            foreach ( string c in nums) {
                toReturn.Add(int.Parse(c));
            }
            return toReturn;
        }
        private BoughtItem convertItem(Item i, int orderID){
            return new BoughtItem {
                ID = i.ID,
                Name = i.Name,
                Description = i.Description,
                Location = i.Location,
                OrderID = orderID,
                Price = i.Price
            } ;
        }
    }
}
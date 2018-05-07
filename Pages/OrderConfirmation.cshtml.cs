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
    public class OrderConfirmModel : PageModel
    {
        private readonly DDSWebstore.Models.MyDBContext _context;

        public OrderConfirmModel(DDSWebstore.Models.MyDBContext context)
        {
            _context = context;
        }

        public List<int> cookieResults;
        public static string ddsCookie;
        public IList<Item> Items { get;set; }

        public IList<BoughtItem> BoughtItem {get; set;}
        public Order Order;
        [HttpPost, HttpGet]
        public async Task<IActionResult> OnGetAsync(string name, string address,
         string city, string state, int zip)
        {

            ddsCookie = Request.Cookies["ddsCookie"];
            Console.WriteLine(" INSIDE ASYNC TASK POST");
            var items = _context.Item.Where(t => cookieResults.Contains(t.ID));
            this.cookieResults = this.parseCookieResults(CartModel.ddsCookie);//ddsCookie.Split(',').ToList();

            float totPrice = 0;
            foreach (Item i in items){
                totPrice += i.Price;
            }
            Order = new Order{
                Name = name,
                StreetAddress = address,
                City = city,
                State = state,
                Zipcode = zip,
                Price = (float) Math.Round(totPrice,2)
            };
            _context.Order.Add(Order);
            foreach (Item i in items){
                _context.BoughtItem.Add(convertItem(i,Order.ID));
                _context.Item.Remove(i);
            }
            await _context.SaveChangesAsync();
            return Page();
        }

        private BoughtItem convertItem(Item i, int orderID){
            return new BoughtItem {
                ID = i.ID,
                Name = i.Name,
                Description = i.Description,
                Location = i.Location,
                OrderID = orderID,
                Price = i.Price
            };
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
    }
}

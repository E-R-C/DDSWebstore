using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace DDSWebstore.Controllers {
    public class CookiesController : Controller {
        public IActionResult Index(){
            return View();
        }

        [HttpPost]
        public IActionResult writeCookie(string name, float price) {
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append(name, price.ToString(), cookie);
            ViewBag.message = "Cookies added successfully!";
            return View("Index");

        }

        public IActionResult readCookie() {
            ViewBag.cookievalue = Request.Cookies[""];
            return View();

        }
    }
}
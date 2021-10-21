using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Daimler.Models;
using Daimler.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using static Daimler.Services.DataReader;

namespace Daimler.Pages
{
    public class LoginModel : PageModel
    {
        //DaimlerContext _Context;
        //public LoginModel(DaimlerContext databasecontext)
        //{
        //    _Context = databasecontext;
        //}
        [BindProperty]
        public string Username { get; set; }
        public string Password { get; set; }
        public int PIN { get; set; }
        public bool RememberMe { get; set; }
        //public string Message { get; set; }
        [BindProperty]
        public Toaster myToaster { get; set; }
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostLogin()
        {
            IRestResponse restResponse = new RestResponse();
            DataReader datareader = new DataReader();
            DaimlerContext _context = new DaimlerContext();

            string username = Request.Form["Username"].ToString();
            string password = Request.Form["Password"].ToString();

            var login = await _context.Logins
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

            if (login != null)
            {
                HttpContext.Session.SetString("LoginID", login.Id.ToString());
                return RedirectToPage("/Dashboard");
            }
            else
            {
                myToaster.Message = "Invalid Login details";
                myToaster.CssClass = "alert-danger";
                // ModelState.AddModelError("Error", restResponse.ErrorMessage);

                return Page();
                //if (restResponse.ResponseStatus == ResponseStatus.Error) return BadRequest(error);
            }

        }
    }
}

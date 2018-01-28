using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample.Models;


using Microsoft.AspNetCore.Authentication; //鉴定
using Microsoft.AspNetCore.Authorization;// 授权
using Microsoft.AspNetCore.Authentication.Cookies;//
using System.Security.Claims;


namespace MvcCookieAuthSample.Controllers
{
  
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            var claims  = new List<Claim>{
                new Claim(ClaimTypes.Name,"atwind"),
                new Claim(ClaimTypes.Role,"admin"),
            };

            var claimIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimIdentity));
            
             return Ok(); //变成一个API
            //return View();
        }


        public IActionResult Logout(){

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(); //变成一个API
            //return View();
        }

    }

}

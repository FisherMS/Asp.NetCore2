using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample.Models;
//using Microsoft.AspNetCore.Authentication; //鉴定
using Microsoft.AspNetCore.Authorization;// 授权


namespace MvcCookieAuthSample.Controllers
{
    [Authorize] //表示访问需要授权
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}

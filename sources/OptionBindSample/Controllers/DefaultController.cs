using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OptionBindSample.Controllers
{
    public class DefaultController : Controller
    {

        /// <summary>
        /// 直接从View中读取 IOC进来的实例
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
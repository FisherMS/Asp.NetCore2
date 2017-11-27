using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace OptionBindSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly Class _cls;

        public HomeController(IOptions<Class> clsAccesser)
        {
            _cls = clsAccesser.Value;
        }


        public IActionResult Index()
        {
            return View(_cls);
        }


    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class ToolsController : Controller
    {
        public IActionResult ImagesConvertor()
        {
            return View();
        }
    }
}

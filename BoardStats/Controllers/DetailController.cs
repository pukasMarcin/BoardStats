using BoardStats.Data.Services;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Xml;
using BoardStats.Utility;
using Newtonsoft.Json.Linq;
using BoardStats.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using CheckBoxList.Core.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace BoardStats.Controllers
{
    [Authorize]
    public class DetailController : Controller
    {

        private readonly IDetailService _service;


        public DetailController(IDetailService service)
        {
            _service = service;

        }

        [HttpGet]
        public IActionResult Index(int Id)
        {
            var result = _service.GetMatchById(Id);

            return View(result);
        }
    }
}



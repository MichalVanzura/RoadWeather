using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoadWeather.Controllers
{
    public class TemplatesController : Controller
    {
        public ViewResult ViewMarkerTemplate()
        {
            return View();
        }
    }
}
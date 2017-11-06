using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRiVote.Controllers
{
    public class MenuController : Controller
    {
        Entity db;
        public MenuController()
        {
            db = new Entity();
        }
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
      

    }
}
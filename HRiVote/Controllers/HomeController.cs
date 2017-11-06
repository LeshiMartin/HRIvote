using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DojoTree.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Tree supporting CRUD operations Using Dojo Tree, Entity Framework, Asp .Net MVC";
            return View();
        }

        public ActionResult generateRoot()
        {
            try
            {
                Entity db = new Entity();
                Node node = new Node();

                node = db.Nodes.Find(1);
                if (node == null)
                {
                    //If you deleted Root manually, this couldn't make Root again
                    //because Root Id must be "1", so you must drop the 
                    //Tree table and rebuild it
                    //or change the Root Id in "tree.js"

                    Node rootNode = new Node();
                    rootNode.Name = "Uploads";
                    rootNode.ParentID = 0;
                    db.Nodes.Add(rootNode);

                    db.SaveChanges();
                    ViewBag.Message = "Some Nodes have been generated";
                }
                else { ViewBag.Message = "Root Exists."; }
            }
            catch { ViewBag.Message = "An Error occurred"; }
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using System.Data;
using System.Net;
using HRiVote.Filters;
using HRiVote.DAL;
using HRiVote.Models;
using System.IO;
using HRiVote.ViewModels;

namespace DojoTree.Controllers
{
    public class TreeController : Controller
    {
        private Entity db = new Entity();

        // GET     /Tree/Data/3
        public ActionResult Index(string name)
        {
            string path = Server.MapPath("~/Managment/");
            List<string> picFolders = new List<string>();

           // if (Directory.GetFiles(path, "*").Length > 0)
                picFolders.Add(path);

            foreach (string dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                //if (Directory.GetFiles(dir, "*").Length > 0)
                    picFolders.Add(dir);
            }
            picFolders.RemoveAt(0);
            var viewmodel = new FileViewModel()
            {
                pics = picFolders,
                files = "~/Managment/" + name,
                name=name
            };
           
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file ,string files)
        {

            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath(files), name);
                file.SaveAs(path);
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Error = "You must select a file for download";
                return RedirectToAction("Index");
            }
           
        }
        public ActionResult Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return RedirectToAction("Index");
        }
    }
}
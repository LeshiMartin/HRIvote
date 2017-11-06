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

namespace DojoTree.Controllers
{
    public class TreeController : Controller
    {
        private Entity db = new Entity();

        // GET     /Tree/Data/3
        // POST    /Tree/Data
        // PUT     /Tree/Data/3
        // DELETE  /Tree/Data/3
        [RestHttpVerbFilter]
        public JsonResult Data(Node node, string httpVerb, int id = 0)
        {
            switch (httpVerb)
            {
                case "POST":
                    if (ModelState.IsValid)
                    {
                        db.Entry(node).State = EntityState.Added;
                        db.SaveChanges();
                        return Json(node, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Response.TrySkipIisCustomErrors = true;
                        Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        return Json(new { Message = "Data is not Valid." },
                        JsonRequestBehavior.AllowGet);
                    }
                case "PUT":
                    if (ModelState.IsValid)
                    {
                        db.Entry(node).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(node, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Response.TrySkipIisCustomErrors = true;
                        Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        return Json(new
                        {
                            Message = "Node " + id + "Data is not Valid." }, JsonRequestBehavior.AllowGet);
                        }
                case "GET":
                    try
                    {
                        var node_ = from entity in db.Nodes.Where(x => x.ID.Equals(id))
                                    select new
                                    {
                                        id = entity.ID,
                                        NodeName = entity.Name,
                                        ParentId = entity.ParentID,
                                        children = from entity1 in db.Nodes.Where
                                        (y => y.ParentID.Equals(entity.ID))
                                                   select new
                                                   {
                                                       id = entity1.ID,
                                                       NodeName = entity1.Name,
                                                       ParentId = entity1.ParentID,
                                                       children =
                                                       "" // it calls checking children 
                                                          // whenever needed
                                                   }
                                    };

                        var r = node_.First();
                        return Json(r, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        Response.TrySkipIisCustomErrors = true;
                        Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        return Json(new
                        {
                            Message = "Node " + id +
                        " does not exist."
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "DELETE":
                    try
                    {
                        node = db.Nodes.Single(x => x.ID == id);
                        db.Nodes.Remove(node);
                        db.SaveChanges();
                        return Json(node, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        Response.TrySkipIisCustomErrors = true;
                        Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        return Json(new
                        {
                            Message =
                        "Could not delete Node " + id
                        }, JsonRequestBehavior.AllowGet);
                    }
            }
            return Json(new
            {
                Error = true,
                Message = "Unknown HTTP verb"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
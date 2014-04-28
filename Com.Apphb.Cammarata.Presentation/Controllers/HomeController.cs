using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Com.Apphb.Cammarata.EF.Model;
using Com.Apphb.Cammarata.EF.Persistence;
using Com.Apphb.Cammarata.Presentation.Models;
using MongoDB.Driver;

namespace Com.Apphb.Cammarata.Presentation.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //private readonly MongoCollection<Participant> _participantCollection;

        private  readonly Context _context = new Context();
        
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(_context.Users);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
	}
}
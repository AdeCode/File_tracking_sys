using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Registry.Models;

namespace Registry.Controllers
{
    public class ChargeController : Controller
    {
        private RegisterEntities db = new RegisterEntities();

        // GET: /Charge/
        public ActionResult Index()
        {
            return View(db.charges.ToList());
        }

        // GET: /Charge/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            charge charge = db.charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        // GET: /Charge/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Charge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,number,destination,page,date")] charge charge)
        {
            if (ModelState.IsValid)
            {
                db.charges.Add(charge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(charge);
        }

        // GET: /Charge/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            charge charge = db.charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        // POST: /Charge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,number,destination,page,date")] charge charge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(charge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(charge);
        }

        // GET: /Charge/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            charge charge = db.charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        // POST: /Charge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            charge charge = db.charges.Find(id);
            db.charges.Remove(charge);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult visible()
        {
            ViewBag.Message="Is this visible enough";
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

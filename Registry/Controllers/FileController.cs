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
    public class FileController : Controller
    {
        private RegisterEntities db = new RegisterEntities();

        // GET: /File/
        public ActionResult Index()
        {
            return View(db.files.ToList());
        }

        // GET: /File/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = db.files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: /File/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /File/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,title,number")] file file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.files.Add(file);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View(file);
        }

        // GET: /File/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = db.files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: /File/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,title,number")] file file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        // GET: /File/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = db.files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: /File/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            file file = db.files.Find(id);
            db.files.Remove(file);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowChargedFile()
        {
            return View(db.charges.ToList());
        }

        public ActionResult ChargeFile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChargeFile([Bind(Include = "id,number,destination,page,date,ChargedBy")] charge Charge)
        {
            string holdUser = "";
            Charge.date = DateTime.Now.Date;
            var currentUser = TempData["currentUser"];
            holdUser = currentUser.ToString();
            Charge.ChargedBy = holdUser;
            Archive archive = db.Archives.Find(Charge.number);
            if (archive != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.charges.Add(Charge);
                        db.SaveChanges();
                        db.Archives.Remove(archive);
                        db.SaveChanges();
                        return RedirectToAction("ShowChargedFile");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else
            {
                Response.Write("<script>alert('File not in archive')</script>");                          
            }                          
            return View(Charge);
        }

       
        public ActionResult EditArchivedFile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archive archive = db.Archives.Find(id);
            if (archive == null)
            {
                return HttpNotFound();
            }
            return View(archive);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArchivedFile([Bind(Include = "id,fileNumber,status,cabinet")] Archive archive) 
        {
            if (ModelState.IsValid)
            {
                db.Entry(archive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowArchivedFiles");
            }
            return View(archive);
        }
        public ActionResult ChargedFileindex(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            charge Charge = db.charges.Find(id);
            if (Charge == null)
            {
                return HttpNotFound();
            }
            return View(Charge);
        }

        public ActionResult DeleteChargedFile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            charge Charge = db.charges.Find(id);
            if (Charge == null)
            {
                return HttpNotFound();
            }
            return View(Charge);
        }

        [HttpPost, ActionName("DeleteChargedFile")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteChargedFileConfirmed(int id)
        {
            charge Charge = db.charges.Find(id);
            db.charges.Remove(Charge);
            db.SaveChanges();
            return RedirectToAction("ShowChargedFile");
        }

        public ActionResult ShowArchivedFiles()
        {
            return View(db.Archives.ToList());
        }
        public ActionResult ArchiveFile()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveFile(Archive archive)
        {
            Archive getFile = db.Archives.Find(archive.fileNumber);
            if (getFile == null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Archives.Add(archive);
                        db.SaveChanges();
                        return RedirectToAction("ShowArchivedFiles");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else
            {
                Response.Write("<script>alert('File already in archive')</script>");
            }
               
            return View();

        }

        public ActionResult SearchFile()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeArchive(Archive archive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowArchivedFiles");
            }
            return View(archive);
        }

        public ActionResult ArchiveDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archive archive = db.Archives.Find(id);
            if (archive == null)
            {
                return HttpNotFound();
            }
            return View(archive);
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

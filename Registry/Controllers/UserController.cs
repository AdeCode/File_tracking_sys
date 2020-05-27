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
    public class UserController : Controller
    {
        private RegisterEntities db = new RegisterEntities();

        //GET: /user/
        public ActionResult ShowUser()
        {
            return View(db.users.ToList());
        }


        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "id,userId,lastName,firstName,middleName,phoneNumber,password")] user user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Registered successfully" }, JsonRequestBehavior.AllowGet);
                    return RedirectToAction("ShowUser");
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View(user);
        }

        public JsonResult CheckUsernameAvailability(string username)
        {
            System.Threading.Thread.Sleep(200);
                var searchDb = db.users.Where(x => x.userId == username).SingleOrDefault();
                if (searchDb != null)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0); 
                }
        }
        public ActionResult UserDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // GET: /User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,userId,lastName,firstName,middleName,phoneNumber")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,userId,lastName,firstName,middleName,phoneNumber")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserLogin()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(user User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (RegisterEntities info = new RegisterEntities())
                    {
                        var CurrentUser = info.users.Where(a => a.userId.Equals(User.userId) && a.password.Equals(User.password)).FirstOrDefault();
                        if (CurrentUser != null)
                        {
                           
                            Session["userId"] = User.userId;
                            TempData["currentUser"] = User.userId;
                            var holdUser = User.userId;
                            return RedirectToAction("DashBoard");
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid username or password')</script>");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
           
            return View();
        }

        public ActionResult show()
        {
            return View(db.users.ToList());
        }

        public ActionResult GetData(){
            using (RegisterEntities db = new RegisterEntities())
            {
                List<user> userList = db.users.ToList();
                return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult UserLogOut()
        {
            return RedirectToAction("UserLogin");
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

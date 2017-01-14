using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminSiteMVC.Models;

namespace AdminSiteMVC.Controllers
{
    public class accessesController : Controller
    {
        private Security_Policies1Entities db = new Security_Policies1Entities();

        // GET: accesses
        public ActionResult Index()
        {
            var accesses = db.accesses.Include(a => a.policy).Include(a => a.user).Include(a => a.version);
            return View(accesses.ToList());
        }

        // GET: accesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            access access = db.accesses.Find(id);
            if (access == null)
            {
                return HttpNotFound();
            }
            return View(access);
        }

        // GET: accesses/Create
        public ActionResult Create()
        {
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id");
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id");
            ViewBag.v_id = new SelectList(db.versions, "v_id", "v_id");
            return View();
        }

        // POST: accesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ac_id,v_id,p_id,u_id,date")] access access)
        {
            if (ModelState.IsValid)
            {
                db.accesses.Add(access);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", access.p_id);
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", access.u_id);
            ViewBag.v_id = new SelectList(db.versions, "v_id", "v_id", access.v_id);
            return View(access);
        }

        // GET: accesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            access access = db.accesses.Find(id);
            if (access == null)
            {
                return HttpNotFound();
            }
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", access.p_id);
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", access.u_id);
            ViewBag.v_id = new SelectList(db.versions, "v_id", "v_id", access.v_id);
            return View(access);
        }

        // POST: accesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ac_id,v_id,p_id,u_id,date")] access access)
        {
            if (ModelState.IsValid)
            {
                db.Entry(access).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", access.p_id);
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", access.u_id);
            ViewBag.v_id = new SelectList(db.versions, "v_id", "v_id", access.v_id);
            return View(access);
        }

        // GET: accesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            access access = db.accesses.Find(id);
            if (access == null)
            {
                return HttpNotFound();
            }
            return View(access);
        }

        // POST: accesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            access access = db.accesses.Find(id);
            db.accesses.Remove(access);
            db.SaveChanges();
            return RedirectToAction("Index");
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

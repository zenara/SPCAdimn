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
    public class policiesController : Controller
    {
        private Security_Policies1Entities db = new Security_Policies1Entities();

        // GET: policies
        public ActionResult Index()
        {
            var policies = db.policies.Include(p => p.user);
            return View(policies.ToList());
        }

        // GET: policies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            policy policy = db.policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            return View(policy);
        }

        // GET: policies/Create
        public ActionResult Create()
        {
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id");
            return View();
        }

        // POST: policies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,p_no,u_id")] policy policy)
        {
            if (ModelState.IsValid)
            {
                db.policies.Add(policy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", policy.u_id);
            return View(policy);
        }

        // GET: policies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            policy policy = db.policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", policy.u_id);
            return View(policy);
        }

        // POST: policies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,p_no,u_id")] policy policy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", policy.u_id);
            return View(policy);
        }

        // GET: policies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            policy policy = db.policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            return View(policy);
        }

        // POST: policies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            policy policy = db.policies.Find(id);
            db.policies.Remove(policy);
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

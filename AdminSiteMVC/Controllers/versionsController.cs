﻿using System;
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
    public class versionsController : Controller
    {
        private Security_Policies1Entities db = new Security_Policies1Entities();

        // GET: versions
        public ActionResult Index()
        {
            var versions = db.versions.Include(v => v.policy).Include(v => v.user);
            return View(versions.ToList());
        }

        //Changed to get the relevant sections of the version
        // GET: versions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var versions = db.versions.Include(v => v.policy).Include(v => v.user);
            var version = db.versions.Find(id);
            var sections = version.sections.ToList();
            if (version == null)
            {
                return HttpNotFound();
            }
            return View(sections);
        }

        // GET: versions/Create
        public ActionResult Create()
        {
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id");
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id");
            return View();
        }

        // POST: versions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "v_id,v_no,u_id,p_id")] version version)
        {
            if (ModelState.IsValid)
            {
                db.versions.Add(version);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", version.p_id);
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", version.u_id);
            return View(version);
        }

        // GET: versions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            version version = db.versions.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", version.p_id);
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", version.u_id);
            return View(version);
        }

        // POST: versions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "v_id,v_no,u_id,p_id")] version version)
        {
            if (ModelState.IsValid)
            {
                db.Entry(version).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", version.p_id);
            ViewBag.u_id = new SelectList(db.users, "u_id", "u_id", version.u_id);
            return View(version);
        }

        // GET: versions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            version version = db.versions.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            return View(version);
        }

        // POST: versions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            version version = db.versions.Find(id);
            db.versions.Remove(version);
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

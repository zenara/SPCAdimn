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
    public class sectionsController : Controller
    {
        private Security_Policies1Entities db = new Security_Policies1Entities();

        // GET: sections
        public ActionResult Index()
        {
            var sections = db.sections.Include(s => s.policy).Include(s => s.version);
            return View(sections.ToList());
        }

        // GET: sections/CurrentPolicy
        public ActionResult CurrentPolicy()
        {
            var versions = db.versions.Include(v => v.policy).Include(v => v.user);
            var last = versions.OrderByDescending(v => v.v_id).FirstOrDefault();
            var sections = last.sections.ToList();
            if (last == null)
            {
                return HttpNotFound();
            }
            return View(sections);
        }

        // GET: sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            section section = db.sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: sections/Create
        public ActionResult Create()
        {
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id");
            ViewBag.v_id = new SelectList(db.versions, "v_id", "v_id");
            return View();
        }

        // POST: sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sec_id,p_id,v_id,content,sec_no")] section section)
        {
            if (ModelState.IsValid)
            {
                db.sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", section.p_id);
            ViewBag.v_id = new SelectList(db.versions, "v_id", "v_id", section.v_id);
            return View(section);
        }

        // GET: sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            section section = db.sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", section.p_id);
            ViewBag.v_id = new SelectList(db.versions, "v_id", "v_id", section.v_id);
            return View(section);
        }

        // POST: sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sec_id,p_id,v_id,content,sec_no")] section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.p_id = new SelectList(db.policies, "p_id", "p_id", section.p_id);
            ViewBag.v_id = new SelectList(db.versions, "v_id", "v_id", section.v_id);
            return View(section);
        }

        // GET: sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            section section = db.sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            section section = db.sections.Find(id);
            db.sections.Remove(section);
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

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using BugTracker.Models.Classes;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Admin,Project Manager")]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }
        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }
        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AssignUsers(int id)
        {
            var model = new ProjectAssignViewModel();
            model.Id = id;
            var project = db.Projects.FirstOrDefault(p => p.Id == id);
            var users = db.Users.ToList();
            model.UserList = new MultiSelectList(users,"Id","FinalName",project.Users.Select(p=>p.Id).ToList());
            return View(model);
        }
        [HttpPost]
        public ActionResult AssignUsers(ProjectAssignViewModel model)
        {
            var project = db.Projects.FirstOrDefault(p => p.Id == model.Id);
            var assignUsers = project.Users.ToList();
            foreach(var user in assignUsers)
            {
                project.Users.Remove(user);
            }
            if(model.SelectedUsers != null)
            {
            foreach(var userId in model.SelectedUsers)
              {
                var user = db.Users.FirstOrDefault(p => p.Id == userId);
                project.Users.Add(user);
              }
            }
           
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{   [Authorize]
    public class TodoTablesController : Controller
    {
        private TodoListDbEntities db = new TodoListDbEntities();

        // GET: TodoTables
        public ActionResult Index()
        {
            int userId =  Convert.ToInt32(Session["UserId"]);
            var todoTables = db.TodoTables.Include(t => t.UserInfo).Where(t => t.UserId == userId);
            return View(todoTables.ToList());
        }

        // GET: TodoTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoTable todoTable = db.TodoTables.Find(id);
            if (todoTable == null)
            {
                return HttpNotFound();
            }
            return View(todoTable);
        }

        // GET: TodoTables/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserInfoes, "UserId", "UserName");
            return View();
        }

        // POST: TodoTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TodoId,Subject,Body,Date,UserId")] TodoTable todoTable)
        {
            if (ModelState.IsValid)
            {
                db.TodoTables.Add(todoTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserInfoes, "UserId", "UserName", todoTable.UserId);
            return View(todoTable);
        }

        // GET: TodoTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoTable todoTable = db.TodoTables.Find(id);
            if (todoTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserInfoes, "UserId", "UserName", todoTable.UserId);
            return View(todoTable);
        }

        // POST: TodoTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TodoId,Subject,Body,Date,UserId")] TodoTable todoTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todoTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserInfoes, "UserId", "UserName", todoTable.UserId);
            return View(todoTable);
        }

        // GET: TodoTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoTable todoTable = db.TodoTables.Find(id);
            if (todoTable == null)
            {
                return HttpNotFound();
            }
            return View(todoTable);
        }

        // POST: TodoTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodoTable todoTable = db.TodoTables.Find(id);
            db.TodoTables.Remove(todoTable);
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

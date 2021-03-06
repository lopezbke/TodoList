﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class UserInfoController : Controller
    {
        
        private TodoListDbEntities db = new TodoListDbEntities();

        // GET: UserInfo
       /* public ActionResult Index()
        {
            return View(db.UserInfoes.ToList());
        }*/

        // GET: UserInfo/Details/5
        public ActionResult Login() 
        {
            
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,Password")] UserInfo userInfo)
        {
            if (ModelState.IsValid) 
            {
                var a = db.UserInfoes.Where(u => u.UserName == userInfo.UserName && u.Password == userInfo.Password);
                string userName = null;
                string password = null;
                string email = null;
                int userId = 0;
                foreach (var item in a)
                {
                    System.Diagnostics.Debug.WriteLine(item.UserName);
                    System.Diagnostics.Debug.WriteLine(item.Password);
                    System.Diagnostics.Debug.WriteLine(item.Email);
                    userId = item.UserId;
                    userName = item.UserName;
                    password = item.Password;
                    email = item.Email;
                }
                if (userName != "" && userName != null) 
                {
                    FormsAuthentication.SetAuthCookie(userId.ToString(), false);
                    Session.Add("UserId", userId.ToString());

                    FormsAuthentication.SetAuthCookie(userName, false);
                    Session.Add("UserName", userName);
                }
                
            }
            

           
            return RedirectToAction("Index","TodoTables");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: UserInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Password,Email")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.UserInfoes.Add(userInfo);
                db.SaveChanges();
                return RedirectToAction("Index","TodoTables");
            }

            return View(userInfo);
        }

        // GET: UserInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Password,Email")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }

        // GET: UserInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.UserInfoes.Find(id);
            db.UserInfoes.Remove(userInfo);
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

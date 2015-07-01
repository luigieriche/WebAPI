using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UsuariosController : Controller
    {
        private NestAppEntities db = new NestAppEntities();

        // GET: /Usuarios/
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.AspNetUsers);
            return View(usuarios.ToList());
        }

        // GET: /Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);

        }
        // GET: /Usuarios/GetUsuarioDetalhe/5
        public ActionResult GetUsuarioDetalhe(int id)
        {
            var res = new Newtonsoft.Json.Linq.JObject();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            JArray array = new JArray();
            array.Add(usuario.ID);
            array.Add(usuario.AspNetUsers.UserName);
            array.Add(usuario.Nome);
            res["id"] = 1;
            res["result"] = array;
            return Content(res.ToString(), "application/json");
        }

            

        // GET: /Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IDAspNetUsers = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: /Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,IDAspNetUsers,Nome")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDAspNetUsers = new SelectList(db.AspNetUsers, "Id", "UserName", usuarios.IDAspNetUsers);
            return View(usuarios);
        }

        // GET: /Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDAspNetUsers = new SelectList(db.AspNetUsers, "Id", "UserName", usuarios.IDAspNetUsers);
            return View(usuarios);
        }

        // POST: /Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,IDAspNetUsers,Nome")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDAspNetUsers = new SelectList(db.AspNetUsers, "Id", "UserName", usuarios.IDAspNetUsers);
            return View(usuarios);
        }

        // GET: /Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: /Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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

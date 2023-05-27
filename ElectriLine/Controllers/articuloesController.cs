using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElectriLine.Models;

namespace ElectriLine.Controllers
{
    public class articuloesController : Controller
    {
        private ElectricLineEntities1 db = new ElectricLineEntities1();

        // GET: articuloes
        public ActionResult Index()
        {
            var articulo = db.articulo.Include(a => a.categoria);
            return View(articulo.ToList());
        }

        // GET: articuloes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // GET: articuloes/Create
        public ActionResult Create()
        {
            ViewBag.idcategoria = new SelectList(db.categoria, "idcategoria", "nombre");
            return View();
        }

        // POST: articuloes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idarticulo,idcategoria,codigo,nombre,precio_venta,stock,descripcion,estado")] articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.articulo.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcategoria = new SelectList(db.categoria, "idcategoria", "nombre", articulo.idcategoria);
            return View(articulo);
        }

        // GET: articuloes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcategoria = new SelectList(db.categoria, "idcategoria", "nombre", articulo.idcategoria);
            return View(articulo);
        }

        // POST: articuloes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idarticulo,idcategoria,codigo,nombre,precio_venta,stock,descripcion,estado")] articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcategoria = new SelectList(db.categoria, "idcategoria", "nombre", articulo.idcategoria);
            return View(articulo);
        }

        // GET: articuloes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: articuloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            articulo articulo = db.articulo.Find(id);
            db.articulo.Remove(articulo);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetroWeb.AuthFolder;
using MetroWeb.Models;

namespace MetroWeb.Controllers
{
    [UserAuthentication]
    public class ProblemaController : Controller
    {
        private BDGestionIntegradaEntitiesContext db = new BDGestionIntegradaEntitiesContext();

        // GET: Problema
        public ActionResult Index()
        {
            ViewBag.RutaLayout = VariablesGlobales.LoggedInLayoutPath;
            var tb_Plan_Problema = db.tb_Plan_Problema.Include(t => t.tb_Orga_Area).Include(t => t.tb_Orga_Usuarios).Include(t => t.tb_Plan_Origen).Include(t => t.tb_Plan_Tipo_Problema);
            return View(tb_Plan_Problema.ToList());
        }

        // GET: Problema/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Plan_Problema tb_Plan_Problema = db.tb_Plan_Problema.Find(id);
            if (tb_Plan_Problema == null)
            {
                return HttpNotFound();
            }
            return View(tb_Plan_Problema);
        }

        // GET: Problema/Create
        [HttpGet]
        public ActionResult Create()
        {
            var usuarios = db.tb_Orga_Usuarios.Select(u => new { Nombres = u.Nombres +" " + u.Apellidos, u.Id_Usuario });
            ViewBag.id_area = new SelectList(db.tb_Orga_Area, "Id_Area", "Descripcion_Area");                          
            ViewBag.id_usuario_responsable = new SelectList(usuarios, "Id_Usuario", "Nombres");
            ViewBag.id_origen = new SelectList(db.tb_Plan_Origen, "id_origen", "descripcion");
            ViewBag.id_tipo_problema = new SelectList(db.tb_Plan_Tipo_Problema, "id_tipo_problema", "descripcion");
            return PartialView("_Agregar");
        }

        // POST: Problema/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_problema,descripcion,id_area,id_origen,id_tipo_problema,id_usuario_responsable,flag_eliminado")] tb_Plan_Problema value)
        {
            if (ModelState.IsValid)
            {
                value.flag_eliminado="1";
                db.tb_Plan_Problema.Add(value);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var usuarios = db.tb_Orga_Usuarios.Select(u => new { Nombres = u.Nombres + " " + u.Apellidos, u.Id_Usuario });

            ViewBag.id_area = new SelectList(db.tb_Orga_Area, "Id_Area", "Descripcion_Area", value.id_area);
            ViewBag.id_usuario_responsable = new SelectList(usuarios, "Id_Usuario", "Nombres", value.id_usuario_responsable);
            ViewBag.id_origen = new SelectList(db.tb_Plan_Origen, "id_origen", "descripcion", value.id_origen);
            ViewBag.id_tipo_problema = new SelectList(db.tb_Plan_Tipo_Problema, "id_tipo_problema", "descripcion", value.id_tipo_problema);
            return PartialView("_Agregar", value);
        }

        // GET: Problema/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Plan_Problema tb_Plan_Problema = db.tb_Plan_Problema.Find(id);
            if (tb_Plan_Problema == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_area = new SelectList(db.tb_Orga_Area, "Id_Area", "Descripcion_Area", tb_Plan_Problema.id_area);
            ViewBag.id_usuario_responsable = new SelectList(db.tb_Orga_Usuarios, "Id_Usuario", "Nombres", tb_Plan_Problema.id_usuario_responsable);
            ViewBag.id_origen = new SelectList(db.tb_Plan_Origen, "id_origen", "descripcion", tb_Plan_Problema.id_origen);
            ViewBag.id_tipo_problema = new SelectList(db.tb_Plan_Tipo_Problema, "id_tipo_problema", "descripcion", tb_Plan_Problema.id_tipo_problema);
            return View(tb_Plan_Problema);
        }

        // POST: Problema/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_problema,descripcion,id_area,id_origen,id_tipo_problema,id_usuario_responsable,flag_eliminado")] tb_Plan_Problema tb_Plan_Problema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Plan_Problema).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_area = new SelectList(db.tb_Orga_Area, "Id_Area", "Descripcion_Area", tb_Plan_Problema.id_area);
            ViewBag.id_usuario_responsable = new SelectList(db.tb_Orga_Usuarios, "Id_Usuario", "Nombres", tb_Plan_Problema.id_usuario_responsable);
            ViewBag.id_origen = new SelectList(db.tb_Plan_Origen, "id_origen", "descripcion", tb_Plan_Problema.id_origen);
            ViewBag.id_tipo_problema = new SelectList(db.tb_Plan_Tipo_Problema, "id_tipo_problema", "descripcion", tb_Plan_Problema.id_tipo_problema);
            return View(tb_Plan_Problema);
        }

        // GET: Problema/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Plan_Problema tb_Plan_Problema = db.tb_Plan_Problema.Find(id);
            if (tb_Plan_Problema == null)
            {
                return HttpNotFound();
            }
            return View(tb_Plan_Problema);
        }

        // POST: Problema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Plan_Problema tb_Plan_Problema = db.tb_Plan_Problema.Find(id);
            db.tb_Plan_Problema.Remove(tb_Plan_Problema);
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

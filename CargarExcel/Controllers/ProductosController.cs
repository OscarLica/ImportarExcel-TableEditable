using CargarExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargarExcel.Controllers
{
    public class ProductosController : Controller
    {
        private Identity.Context db = new Identity.Context();
        // GET: Productos
        public ActionResult Index()
        {
            return View(db.productos.ToList());
        }
        [HttpPost]
        public ActionResult loadData(HttpPostedFileBase file)
        {
            string mensaje = string.Empty;
            if (file == null)
            {
                ViewBag.mensaje = false;
            }
            else {
                new BL.BLProductos().saveFile(file);
                ViewBag.mensaje = true;
            }
            return RedirectToAction("Index");
        }
        public JsonResult Editar( Productos p)
        {
            if (p.id != 0)
            {
                if (ModelState.IsValid)
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

            }
         return Json(p,JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateAll(List< Productos> p)
        {
            p.ForEach(x =>
            {
                if (x.id != 0)
                    if (ModelState.IsValid)
                        db.Entry(x).State = System.Data.Entity.EntityState.Modified;
            });         
            return Json(p, JsonRequestBehavior.AllowGet);
        }
    }
}
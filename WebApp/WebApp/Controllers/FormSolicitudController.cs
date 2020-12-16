using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfilEstudiante.Controllers
{
    public class FormSolicitudController : Controller
    {

        [HttpGet]
        public ActionResult Solicitud (string email){
            //autocompletar los datos del usuario buscando el email}
            
            return ();
        }
        // GET: SolicitudController
        [HttpPost]
        public ActionResult Solicitud(string email)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext() ){
                var oTabla = dbContext.usuarios1.Find(email);
                oTabla.contacto = model.contacto;
                oTabla.nacionalidad = model.nacionalidad;
                oTabla.fecha_nacimiento = model.fecha_nacimiento;
                dbContext.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                dbContext.saveChanges();
            }
            //var oTabla = db.Empleado.Find(email); busca el email del usuario
            //oTabla.nombre = contacto;
            //actualizar los campos del moises para poner la nacionalidad, contacto y fecha_nacimiento
            //db.saveChanges(oTabla);
            //cuando termine de subir todos los datos, el estadoSolicitud de la tabla ServicioSolicitud pase a activo

            return View();
        }

        // GET: SolicitudController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SolicitudController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolicitudController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SolicitudController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SolicitudController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SolicitudController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SolicitudController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

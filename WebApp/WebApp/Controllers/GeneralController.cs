using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfilEstudiante.Controllers
{
	[Microsoft.AspNetCore.Authorization.Authorize]
    public class GeneralController : Controller
    {
        // GET: GeneralController
        public ActionResult PerfilEstudiante()
        {
            return View();
        }

        // GET: GeneralController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GeneralController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneralController/Create
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

        // GET: GeneralController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GeneralController/Edit/5
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

        // GET: GeneralController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GeneralController/Delete/5
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

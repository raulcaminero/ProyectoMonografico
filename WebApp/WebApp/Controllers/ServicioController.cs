﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ServicioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servicio
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Servicio.Include(s => s.Campus).Include(s => s.Carrera).Include(s => s.Escuela).Include(s => s.Estado).Include(s => s.Facultad).Include(s => s.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Servicio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Campus)
                .Include(s => s.Carrera)
                .Include(s => s.Escuela)
                .Include(s => s.Estado)
                .Include(s => s.Facultad)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.Servicio_Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicio/Create
        public IActionResult Create()
        {
            ViewData["Campus_Id"] = new SelectList(_context.Campus, "Id", "Codigo");
            ViewData["Carrera_Id"] = new SelectList(_context.Carreras, "Id", "Codigo");
            ViewData["Escuela_Id"] = new SelectList(_context.Escuelas, "Id", "CodigoEscuela");
            ViewData["Estado_Id"] = new SelectList(_context.Estado, "EstadoId", "EstadoId");
            ViewData["Facultad_Id"] = new SelectList(_context.Facultades, "Id", "Codigo");
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios, "codigo", "NombreCompleto");
            return View();
        }

        // POST: Servicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Servicio_Id,Servicio_Codigo,Servicio_Descripcion,Servicio_FechaInicio,Servicio_FechaCierre,Servicio_Costo,UsuarioCodigo,TipoServicio_Id,Estado_Id,Campus_Id,Facultad_Id,Escuela_Id,Carrera_Id")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Campus_Id"] = new SelectList(_context.Campus, "Id", "Codigo", servicio.Campus_Id);
            ViewData["Carrera_Id"] = new SelectList(_context.Carreras, "Id", "Codigo", servicio.Carrera_Id);
            ViewData["Escuela_Id"] = new SelectList(_context.Escuelas, "Id", "CodigoEscuela", servicio.Escuela_Id);
            ViewData["Estado_Id"] = new SelectList(_context.Estado, "EstadoId", "EstadoId", servicio.Estado_Id);
            ViewData["Facultad_Id"] = new SelectList(_context.Facultades, "Id", "Codigo", servicio.Facultad_Id);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios, "codigo", "NombreCompleto", servicio.UsuarioCodigo);
            return View(servicio);
        }

        // GET: Servicio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["Campus_Id"] = new SelectList(_context.Campus, "Id", "Codigo", servicio.Campus_Id);
            ViewData["Carrera_Id"] = new SelectList(_context.Carreras, "Id", "Codigo", servicio.Carrera_Id);
            ViewData["Escuela_Id"] = new SelectList(_context.Escuelas, "Id", "CodigoEscuela", servicio.Escuela_Id);
            ViewData["Estado_Id"] = new SelectList(_context.Estado, "EstadoId", "EstadoId", servicio.Estado_Id);
            ViewData["Facultad_Id"] = new SelectList(_context.Facultades, "Id", "Codigo", servicio.Facultad_Id);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios, "codigo", "NombreCompleto", servicio.UsuarioCodigo);
            return View(servicio);
        }

        // POST: Servicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Servicio_Id,Servicio_Codigo,Servicio_Descripcion,Servicio_FechaInicio,Servicio_FechaCierre,Servicio_Costo,UsuarioCodigo,TipoServicio_Id,Estado_Id,Campus_Id,Facultad_Id,Escuela_Id,Carrera_Id")] Servicio servicio)
        {
            if (id != servicio.Servicio_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.Servicio_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Campus_Id"] = new SelectList(_context.Campus, "Id", "Codigo", servicio.Campus_Id);
            ViewData["Carrera_Id"] = new SelectList(_context.Carreras, "Id", "Codigo", servicio.Carrera_Id);
            ViewData["Escuela_Id"] = new SelectList(_context.Escuelas, "Id", "CodigoEscuela", servicio.Escuela_Id);
            ViewData["Estado_Id"] = new SelectList(_context.Estado, "EstadoId", "EstadoId", servicio.Estado_Id);
            ViewData["Facultad_Id"] = new SelectList(_context.Facultades, "Id", "Codigo", servicio.Facultad_Id);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios, "codigo", "EstadoId", servicio.UsuarioCodigo);
            return View(servicio);
        }

        // GET: Servicio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Campus)
                .Include(s => s.Carrera)
                .Include(s => s.Escuela)
                .Include(s => s.Estado)
                .Include(s => s.Facultad)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.Servicio_Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.Servicio_Id == id);
        }
    }
}

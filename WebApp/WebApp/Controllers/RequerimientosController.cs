using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Data;
using WebApp.ViewModels.Requerimientos;

namespace WebApp.Controllers
{
	public class RequerimientosController : Controller
	{
		private readonly ApplicationDbContext _context;

		public RequerimientosController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Requerimientos
		public async Task<IActionResult> Index()
		{
			var reqs = await _context.Requerimientos
				.Where(r => r.Estado != Models.EstadoRequerimiento.Eliminado)
				.Where(r => r.Estado != Models.EstadoRequerimiento.Historico)
				.ToListAsync();

			return base.View(reqs);
		}

		// GET: Requerimientos/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var req = await _context.Requerimientos
				.Where(m => m.Id == id)
				.AsNoTracking().FirstOrDefaultAsync();

			if (req == null)
				return NotFound();

			var versiones = await _context.Requerimientos
				.Where(r => r.Codigo == req.Codigo)
				.Where(r => r.Id != req.Id)
				.AsNoTracking().ToListAsync();

			var model = construirViewModel(req);
			model.VersionesAnteriores = versiones.Select(v => construirViewModel(v)).ToList();

			return View(model);
		}

		private ViewRequerimientoViewModel construirViewModel(Requerimiento req)
		{
			var model = new ViewRequerimientoViewModel()
			{
				Id = req.Id,
				Codigo = req.Codigo,
				Titulo = req.Titulo,
				Descripcion = req.Descripcion,
				FechaCreacion = req.FechaCreacion,
				TipoServicio = req.TipoServicio,
				Estado = req.Estado,
			};

			return model;
		}

		// GET: Requerimientos/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Requerimientos/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateRequerimientoViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				var codigo = generarCodigo();

				var req = new Requerimiento()
				{
					Codigo = codigo,
					Titulo = modelo.Titulo,
					Descripcion = modelo.Descripcion,
					Estado = Models.EstadoRequerimiento.Activo,
					TipoServicio = modelo.TipoServicio,
					FechaCreacion = DateTime.Now
				};

				_context.Add(req);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(modelo);
		}

		private string generarCodigo()
		{
			string codigo = null;
			var codigoAnterior = _context.Requerimientos.OrderByDescending(r => r.Id).FirstOrDefault()?.Codigo ?? "R000000";

			do
			{
				var secuencia = Convert.ToInt32(String.Join("", codigoAnterior.Where(char.IsDigit)));
				codigo = "R" + (secuencia + 1).ToString("D5");
				codigoAnterior = codigo;
			}
			while (_context.Requerimientos.Any(r => r.Codigo == codigo));
			return codigo;
		}

		// GET: Requerimientos/Edit/5
		public async Task<IActionResult> Edit(string codigo)
		{
			if (string.IsNullOrWhiteSpace(codigo))
				return NotFound();

			var req = await _context.Requerimientos
				.Where(c => c.Codigo == codigo)
				.OrderByDescending(c => c.Estado) // Intentar tomar el que está activo.
				.ThenBy(c => c.FechaCreacion)
				.FirstOrDefaultAsync();

			if (req == null)
				return NotFound();

			var modelo = new EditRequerimientoViewModel()
			{
				Codigo = req.Codigo,
				Titulo = req.Titulo,
				Descripcion = req.Descripcion,
				TipoServicio = req.TipoServicio,
			};

			return View(modelo);
		}

		// POST: Requerimientos/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditRequerimientoViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				var req = new Requerimiento()
				{
					Codigo = modelo.Codigo,
					Titulo = modelo.Titulo,
					Descripcion = modelo.Descripcion,
					Estado = Models.EstadoRequerimiento.Activo,
					TipoServicio = modelo.TipoServicio,
					FechaCreacion = DateTime.Now
				};

				// Poner las demas versiones del mismo requerimiento como historicos
				var anteriores = _context.Requerimientos.Where(r => r.Codigo == modelo.Codigo).ToList();

				foreach (var anterior in anteriores)
					anterior.Estado = Models.EstadoRequerimiento.Historico;

				_context.Add(req);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(modelo);
		}

		// GET: Requerimientos/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var requerimiento = await _context.Requerimientos
				.Where(r => r.Estado != Models.EstadoRequerimiento.Eliminado)
				.Where(r => r.Estado != Models.EstadoRequerimiento.Historico)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (requerimiento == null)
				return NotFound();

			return View(requerimiento);
		}

		// POST: Requerimientos/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var requerimiento = await _context.Requerimientos.FindAsync(id);
			requerimiento.Estado = Models.EstadoRequerimiento.Eliminado;

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Requerimientos/Inactivate/5
		public async Task<IActionResult> Inactivate(int? id)
		{
			if (id == null)
				return NotFound();

			var requerimiento = await _context.Requerimientos
				.Where(r => r.Estado == Models.EstadoRequerimiento.Activo)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (requerimiento == null)
				return NotFound();

			return View(requerimiento);
		}

		// POST: Requerimientos/Inactivate/5
		[HttpPost, ActionName("Inactivate")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> InactivateConfirmed(int id)
		{
			var requerimiento = await _context.Requerimientos.FindAsync(id);

			if (requerimiento == null)
				return NotFound();
			
			requerimiento.Estado = Models.EstadoRequerimiento.Inactivo;

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// POST: Requerimientos/Activate/5
		public async Task<IActionResult> Activate(int id)
		{
			var requerimiento = await _context.Requerimientos.FindAsync(id);

			if (requerimiento == null)
				return NotFound();
			
			requerimiento.Estado = Models.EstadoRequerimiento.Activo;

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Controllers
{
	[Microsoft.AspNetCore.Authorization.Authorize]
	public class ArchivosController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ArchivosController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Archivo> Cargar(IFormFile file, string modulo, string carpeta)
		{
			if (file == null || file.Length == 0)
				throw new Exception("No ha seleccionado un archivo");

			// Asegurar que la carpeta existe.
			var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", carpeta);
			if (!Directory.Exists(ruta))
				Directory.CreateDirectory(ruta);

			// Generar un nombre para el archivo.
			var nuevoNombreArchivo = generarNombreArchivo();
			var archivo = Path.Combine(ruta, nuevoNombreArchivo);

			// Guardar el archivo.
			using (var stream = new FileStream(archivo, FileMode.Create))
				await file.CopyToAsync(stream);

			// Guardar el registro en la DB.
			var registro = new Archivo()
			{
				Fecha = DateTime.Now,
				NombreArchivo = file.FileName,
				Extension = file.ContentType,
				Modulo = modulo,
				Ruta = archivo
			};
			_context.Archivos.Add(registro);
			await _context.SaveChangesAsync();

			return registro;
		}

		private string generarNombreArchivo()
		{
			string archivo = null;
			var archivoExiste = true;

			while (archivoExiste)
			{
				archivo = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.fffff");
				if (!System.IO.File.Exists(archivo))
					archivoExiste = false;
			}

			return archivo;
		}


		public async Task<IActionResult> Descargar(int idArchivo)
		{
			var archivo = _context.Archivos.Find(idArchivo);
			if (archivo == null)
				return NotFound();

			if (!System.IO.File.Exists(archivo.Ruta))
				return Content("Archivo no encontrado");

			var archivoMemoria = new MemoryStream();
			using (var stream = new FileStream(archivo.Ruta, FileMode.Open))
				await stream.CopyToAsync(archivoMemoria);

			archivoMemoria.Position = 0;
			return File(archivoMemoria, GetContentType(archivo.Extension), archivo.NombreArchivo);
		}

		private string GetContentType(string path)
		{
			var types = GetMimeTypes();
			var ext = Path.GetExtension(path).ToLowerInvariant();
			return types[ext];
		}

		private Dictionary<string, string> GetMimeTypes()
		{
			return new Dictionary<string, string>
			{
				{".txt", "text/plain"},
				{".pdf", "application/pdf"},
				{".doc", "application/vnd.ms-word"},
				{".docx", "application/vnd.ms-word"},
				{".xls", "application/vnd.ms-excel"},
				{".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
				{".png", "image/png"},
				{".jpg", "image/jpeg"},
				{".jpeg", "image/jpeg"},
				{".gif", "image/gif"},
				{".csv", "text/csv"}
			};
		}
	}
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels.Requerimientos.Validations
{
    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

			foreach(var type in _types)
            {
				if (file.ContentType == getContentType(type))
					return null;
			}

            return new ValidationResult($"El documento debe ser de tipo {string.Join(", ", _types)}");
        }

		private string getContentType(string type)
		{
			var mimeTypes = new Dictionary<string, string>
			{
				{"txt", "text/plain"},
				{"pdf", "application/pdf"},
				{"doc", "application/vnd.ms-word"},
				{"docx", "application/vnd.ms-word"},
				{"xls", "application/vnd.ms-excel"},
				{"xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
				{"png", "image/png"},
				{"jpg", "image/jpeg"},
				{"jpeg", "image/jpeg"},
				{"gif", "image/gif"},
				{"csv", "text/csv"}
			};

			return mimeTypes[type];
		}
	}
}

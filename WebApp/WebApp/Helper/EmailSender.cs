using System;
using System.Net;
using System.Net.Mail;

namespace WebApp.Helper
{
	public class EmailSender
	{
		public string Send(string correo, string titulo, string cuerpo)
		{
			var mensaje = "Cumplido";

			MailMessage Correo = new MailMessage();
			Correo.From = new MailAddress("culminare.v2@gmail.com");
			Correo.To.Add(correo);
			Correo.Subject = (titulo);
			Correo.Body = cuerpo;
			Correo.Priority = MailPriority.Normal;

			SmtpClient ServerEmail = new SmtpClient();
			ServerEmail.Credentials = new NetworkCredential("culminare.v2@gmail.com", "UASD1538");
			ServerEmail.Host = "smtp.gmail.com";
			ServerEmail.Port = 587;
			ServerEmail.EnableSsl = true;
			try
			{
				ServerEmail.Send(Correo);
			}
			catch (Exception e)
			{
				Console.Write(e);
			}
			Correo.Dispose();
			return mensaje;
		}

	}
}

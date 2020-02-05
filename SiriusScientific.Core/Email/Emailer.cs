using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace SiriusScientific.Core.Email
{
	public class EmailPackage
	{
		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			using (MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["SMTPuser"], ""))
			{
				mailMessage.Subject = "Employee Information Detail";

				mailMessage.Body = CreateBody();

				mailMessage.IsBodyHtml = true;

				SmtpClient smtp = new SmtpClient();

				smtp.Host = ConfigurationManager.AppSettings["Host"];

				smtp.EnableSsl = true;

				NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["SMTPuser"], ConfigurationManager.AppSettings["SMTPpassword"]);
				smtp.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
				smtp.Credentials = NetworkCred;
				smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
				smtp.Send(mailMessage);
			}
		}

		private string CreateBody()
		{
			string body = string.Empty;
			using (StreamReader reader = new StreamReader("~/EmailTamplate.html"))
			{

				body = reader.ReadToEnd();

			}

			body = body.Replace("{fname}", txtfname.Text); //replacing Parameters

			body = body.Replace("{lname}", txtlname.Text);

			body = body.Replace("{dob}", txtdob.Text);
			body = body.Replace("{post}", txtpost.Text);
			body = body.Replace("{designation}", txtdesignation.Text);

			return body;

		}
    }
	}
}

using C__Mailer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Diagnostics;

namespace C__Mailer.Controllers
{
        public class HomeController : Controller
        {
            private readonly ILogger<HomeController> _logger;

            public HomeController(ILogger<HomeController> logger)
            {
                _logger = logger;
            }

            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            public IActionResult SendEmail(EmailEntity objEmailParameters,
                IFormFile PostedFile)
            {
                var myAppConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                /*var Username = myAppConfig.GetValue<string>("EmailConfig:Username");
                var Password = myAppConfig.GetValue<string>("EmailConfig:Password");
                var Host = myAppConfig.GetValue<string>("EmailConfig:Host");
                var Port = myAppConfig.GetValue<int>("EmailConfig:Port");
                var EmailFrom = myAppConfig.GetValue<string>("EmailConfig:MailFrom");
               */


                var Username = myAppConfig.GetValue<string>("EmailConfig:SmtpUser");
                var Password = myAppConfig.GetValue<string>("EmailConfig:SmtpPass");
                var Host = myAppConfig.GetValue<string>("EmailConfig:SmtpHost");
                var Port = myAppConfig.GetValue<int>("EmailConfig:SmtpPort");
                var EmailFrom = myAppConfig.GetValue<string>("EmailConfig:MailFrom");


                MailMessage message = new MailMessage();
                message.From = new MailAddress(EmailFrom);
                message.To.Add(objEmailParameters.ToEmailAddress.ToString());
                message.Subject = objEmailParameters.Subject;
                message.IsBodyHtml = true;
                message.Body = objEmailParameters.EmailBodyMessage;

                // message.Attachments.Add(new Attachment(PostedFile.OpenReadStream(),
                //   PostedFile.FileName));
                SmtpClient mailClient = new SmtpClient(Host);

                try
                {

                    mailClient.UseDefaultCredentials = false;
                    mailClient.Credentials = new System.Net.NetworkCredential(Username, Password);
                    mailClient.Host = Host;
                    mailClient.Port = Port;
                    mailClient.EnableSsl = true;
                    mailClient.Send(message);
                    ViewBag.Message = "Sent Successfull";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
                finally
                {
                    mailClient.Dispose();
                }
                return View("Index");
            }
        }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPersonalSite.Models;//FamilyMemberViewModel
using System.Net.Mail;//Access to Most Email functionality
using System.Net;//Access to network credentials

namespace MVCPersonalSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactAjax(ContactViewModel cvm)
        {
                string body = $"{cvm.Name} has sent you the following message: <br/> {cvm.Message} <strong> from the email address:</strong> {cvm.Email}";
                MailMessage m = new MailMessage("no-reply@jacobfiler.com", "jacobwilliamfiler@gmail.com", cvm.Subject, body);

                m.IsBodyHtml = true;
                m.Priority = MailPriority.High;
                m.ReplyToList.Add(cvm.Email);

                SmtpClient client = new SmtpClient("mail.jacobfiler.com");
                client.Credentials = new NetworkCredential("no-reply@jacobfiler.com", "Wobuai1!");

                try
                {
                    client.Send(m);
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.StackTrace;
                }
            
            return Json(cvm);
        }
    }
}
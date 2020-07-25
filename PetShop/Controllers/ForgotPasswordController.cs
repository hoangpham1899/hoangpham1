using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class ForgotPasswordController : Controller
    {
        DBPetShopEntities db = new DBPetShopEntities();
        // GET: ForgotPassword
        public ActionResult SendEmail()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(Mail obj)
        {

            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "petshopd17pm@gmail.com";
                WebMail.Password = "petshopd17pm12345";
                User info = db.Users.SingleOrDefault(t => t.Email == obj.ToEmail);
                //Sender email address.  
                WebMail.From = "petshopd17pm@gmail.com";
                obj.EmailSubject = "Reset Password";
                obj.EMailBody = "<a href='https://localhost:44328/ForgotPassword/ResetPassword/" + info.IDUser + "?token=" + info.Token + "'>Reset Password</a>";
                //Send email  
                WebMail.Send(to: obj.ToEmail, subject: obj.EmailSubject, body: obj.EMailBody, cc: obj.EmailCC, bcc: obj.EmailBCC, isBodyHtml: true);
                ViewBag.Status = "Email Sent Successfully.";
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> ResetPassword(int? id, string token)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User us = await db.Users.FindAsync(id);
            Session["fgpw"] = us;
            if (us == null)
            {
                return HttpNotFound();
            }
            return View(us);
        }

        // POST: Admin/TSql_Informations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> ResetPassword([Bind(Include = "IDUser,DisplayName,Email,Password,Token,Role,PhoneNumber,Address,ConfirmPassword")] User us)
        {
            User tmp = (User)Session["fgpw"];
            us.IDUser = tmp.IDUser;
            if (ModelState.IsValid)
            {
                us.Token = Guid.NewGuid().ToString();
                db.Entry(us).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Session["user"] = Session["flag"] = Session["fgpw"];
                Session["ffgpw"] = null;
            }
            return View();
        }
    }
}
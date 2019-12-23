using Stripe;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Voca.BLL.Exceptions;
using Voca.BLL.Helpers;
using Voca.BLL.Managers;
using Voca.Domain.Entities;
using Voca.Presentation.Membership;
using Voca.Presentation.Models;

namespace Voca.Presentation.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ClientManager _clientManager;
        private UserManager _userManager;
        private VocaMembership _membership;

        public ProfileController()
        {
            _clientManager = new ClientManager();
            _userManager = new UserManager();
            _membership = new VocaMembership();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new ProfileViewmodel()
            {
                UserVerified = _membership.CurrentUser.IsVerified,
                UserClients = _clientManager.GetByUserId(_membership.CurrentUser.Id).ToList()
            };

            ViewBag.PaymentPublishableKey = ConfigurationManager.AppSettings["StripePublishableKey"];

            return View(model);
        }


        [HttpPost]
        public ActionResult CreateClient(string clientName, int numberOfMonths, string stripeEmail, string stripeToken)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = numberOfMonths * 389,
                Description = "Client creation",
                Currency = "eur",
                CustomerId = customer.Id
            });

            try
            {
                _clientManager.CreateNew(clientName, DateTime.UtcNow.AddMonths(numberOfMonths), _membership.CurrentUser.Id);
            }
            catch (BusinessException be)
            {
                ///TODO Log error.

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public JsonResult GetClientName(int clientId)
        {
            try
            {
                string clientName = _clientManager.GetById(clientId).Name;

                return Json(new { isSuccessful = true, clientName = clientName }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException be)
            {
                ///TODO Log error.

                return Json(new { isSuccessful = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateClientName(int clientId, string newName)
        {
            try
            {
                _clientManager.UpdateName(clientId, newName);

                return Json(new { isSuccessful = true });
            }
            catch (Exception ex)
            {
                ///TODO Log error.

                return Json(new { isSuccessful = false });
            }
        }

        [HttpPost]
        public JsonResult DeleteClient(int clientId)
        {
            try
            {
                _clientManager.RemoveById(clientId);

                return Json(new { isSuccessful = true, message = "Client successfully deleted!" });
            }
            catch (BusinessException be)
            {
                return Json(new { isSuccessful = true });
            }
        }


        [HttpPost]
        public JsonResult ResetApiKey(int clientId)
        {
            try
            {
                var newKey = _clientManager.ResetApiKey(clientId);

                return Json(new { isSuccessful = true, newKey = newKey });
            }
            catch (BusinessException be)
            {
                ///TODO Log error.

                return Json(new { isSuccessful = false });
            }
        }


        [HttpGet]
        public JsonResult GetCurrentUserEmail()
        {
            string currentEmail = User.Identity.Name;

            return Json(new { currentEmail = currentEmail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEmail(string newEmail)
        {
            try
            {
                User found = _userManager.GetByEmail(_membership.CurrentUser.Email);
                found.Email = newEmail;
                _userManager.Update(found);

                _membership.Clear();
                FormsAuthentication.SetAuthCookie(newEmail, false);

                return Json(new { isSuccessful = true });
            }
            catch (BusinessException be)
            {
                ///TODO Log error.

                return Json(new { isSuccessful = true });
            }
        }

        [HttpPost]
        public JsonResult UpdatePassword(string oldPassword, string newPassword)
        {
            try
            {
                if (_membership.ValidateUser(User.Identity.Name, oldPassword))
                {
                    _membership.CurrentUser.Password = SecurityHelper.GetHash(newPassword);
                    _userManager.Update(_membership.CurrentUser);
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json(new { message = "Your current password is wrong." });
                }

                return Json("Password changed!");
            }
            catch (BusinessException be)
            {
                Response.StatusCode = 400;
                return Json(new { message = "There was an error." });
            }
        }

        [HttpGet]
        public JsonResult DeleteAccount()
        {
            FormsAuthentication.SignOut();

            try
            {
                _userManager.RemoveById(_membership.CurrentUser.Id);

                return Json(new { isSuccessful = true }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException be)
            {
                ///TODO Log error.

                return Json(new { isSuccessful = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
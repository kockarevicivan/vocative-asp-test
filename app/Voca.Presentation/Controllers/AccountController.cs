using System;
using System.Web.Mvc;
using System.Web.Security;
using Voca.BLL.Exceptions;
using Voca.BLL.Managers;
using Voca.Presentation.Membership;
using Voca.Presentation.Models;

namespace Voca.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private UserManager _userManager;
        private VocaMembership _membership;


        public AccountController()
        {
            _userManager = new UserManager();
            _membership = new VocaMembership();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewmodel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_membership.ValidateUser(loginViewModel.Email, loginViewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.Email, loginViewModel.RememberMe == "on");

                    return RedirectToAction("Index", "Profile");
                }
            }

            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewmodel registerViewmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userManager.Register(registerViewmodel.Email, registerViewmodel.Password);

                    FormsAuthentication.SetAuthCookie(registerViewmodel.Email, false);

                    return RedirectToAction("Index", "Home");
                }
                catch (BusinessException be)
                {
                    ///TODO Log error.
                }
            }

            return View();
        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewmodel forgotPasswordViewmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userManager.ResetPassword(forgotPasswordViewmodel.Email);

                    return View("PasswordSent");
                }
                catch (BusinessException be)
                {
                    ///TODO Log error.
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult ForgotPasswordEmailLink(ForgotPasswordEmailModel forgotPasswordEmailModel)
        {
            try
            {
                _userManager.Validate(forgotPasswordEmailModel.Token);

                return RedirectToAction("Index", "Profile");
            }
            catch (BusinessException be)
            {
                ///TODO Log error.

                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
    }
}
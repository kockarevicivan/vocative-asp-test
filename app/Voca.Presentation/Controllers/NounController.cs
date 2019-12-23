using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Voca.BLL.Exceptions;
using Voca.BLL.Managers;
using Voca.Domain.Entities;
using Voca.Presentation.Models;

namespace Voca.Presentation.Controllers
{
    public class NounController : Controller
    {
        private ClientManager _clientManager;
        private NounManager _nounManager;

        public NounController()
        {
            _clientManager = new ClientManager();
            _nounManager = new NounManager();
        }

        public JsonResult Get(List<string> nouns)
        {
            try
            {
                string apiKey = Request.Headers["Authorization"];

                Client found = _clientManager.GetByApiKey(apiKey);

                if (string.IsNullOrEmpty(apiKey))
                {
                    return Json(new ResponseModel
                    {
                        status = "error_key_missing",
                        error_messages = new List<string>() { "Api key is missing in the authorization header." },
                        nouns = null
                    });
                }
                else if (found != null && found.ActiveUntil > DateTime.Now)
                {
                    List<Noun> processed = _nounManager.ProccessRange(nouns);

                    return Json(new ResponseModel
                    {
                        status = "ok",
                        error_messages = null,
                        nouns = processed
                    });
                }
                else
                {
                    return Json(new ResponseModel
                    {
                        status = "error_key_incorrect",
                        error_messages = new List<string>() { "Api key incorrect." },
                        nouns = null
                    });
                }
            }
            catch (BusinessException be)
            {
                ///TODO Log error.

                return Json(new ResponseModel
                {
                    status = "system_error",
                    error_messages = new List<string>() { "There was an internal error while processing your request." },
                    nouns = null
                });
            }
        }

        public JsonResult Test(List<string> fakeNouns)
        {
            string apiKey = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(apiKey))
            {
                return Json(new ResponseModel
                {
                    status = "error_key_missing",
                    error_messages = new List<string>() { "Api key is missing in the authorization header." },
                    nouns = null
                });
            }
            else if (apiKey == "0iGhVoq3SLDvAVhUWM6wROaFoy0BcXKG")
            {
                return Json(new ResponseModel
                {
                    status = "ok",
                    error_messages = null,
                    nouns = new List<Noun>()
                    {
                        new Noun()
                        {
                            Nominative = "Vokativ",
                            Genitive = "Vokativa",
                            Dative = "Vokativu",
                            Accusative = "Vokativ",
                            Vocative = "Vokative",
                            Instrumental = "Vokativom",
                            Locative = "Vokativu",
                            IsGuaranteed = true
                        }
                    }
                });
            }
            else
            {
                return Json(new ResponseModel
                {
                    status = "error_key_incorrect",
                    error_messages = new List<string>() { "Api key incorrect." },
                    nouns = null
                });
            }
        }
    }
}
using System.Collections.Generic;
using Voca.BLL.Helpers;
using Voca.BLL.Services;
using Voca.DAL.Repositories;
using Voca.Domain.Entities;

namespace Voca.BLL.Managers
{
    public class NounManager
    {
        private NounRepository _repository;
        private EmailService _emailService;
        private NounHelper _nounHelper;


        public NounManager()
        {
            _repository = new NounRepository();
            _emailService = new EmailService();
            _nounHelper = new NounHelper();
        }


        public void Add(Noun entity)
        {
            _repository.Add(entity);
        }


        public List<Noun> ProccessRange(List<string> nominatives)
        {
            List<Noun> result = new List<Noun>();

            foreach (var nominative in nominatives)
            {
                Noun current = _repository.GetByNominative(nominative);

                if (current == null)
                {
                    current = new Noun()
                    {
                        Nominative = nominative,
                        Genitive = _nounHelper.GetGenitive(nominative),
                        Dative = _nounHelper.GetDativeOrLocative(nominative),
                        Accusative = _nounHelper.GetAccussative(nominative),
                        Vocative = _nounHelper.GetVocative(nominative),
                        Instrumental = _nounHelper.GetInstrumental(nominative),
                        Locative = _nounHelper.GetDativeOrLocative(nominative),
                        IsGuaranteed = false
                    };

                    Add(current);
                }

                // Notify admin by email.
                _emailService.SendNewNounEmail(nominative);

                result.Add(current);
            }

            return result;
        }
    }
}

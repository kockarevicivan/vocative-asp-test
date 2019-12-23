using System;
using System.Linq;
using Voca.DAL.Exceptions;
using Voca.Domain.Entities;

namespace Voca.DAL.Repositories
{
    public class NounRepository : Repository<Noun>
    {
        public Noun GetByNominative(string nominative)
        {
            return Context.Set<Noun>().Where(n => n.Nominative.ToLower() == nominative.ToLower()).FirstOrDefault(); ;
        }

        public void Update(Noun entity)
        {
            Noun found = this.Context.Set<Noun>().Find(entity.Id);

            // Rewrite all fields (Except Id).
            found.Nominative = entity.Nominative;
            found.Genitive = entity.Genitive;
            found.Dative = entity.Dative;
            found.Accusative = entity.Accusative;
            found.Vocative = entity.Vocative;
            found.Instrumental = entity.Instrumental;
            found.Locative = entity.Locative;
            found.IsGuaranteed = entity.IsGuaranteed;

            try
            {
                this.Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DataException("There was an error while trying to update entity.", e);
            }
        }
    }
}

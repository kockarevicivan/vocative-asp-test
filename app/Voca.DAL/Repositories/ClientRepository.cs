using System;
using System.Collections.Generic;
using System.Linq;
using Voca.DAL.Exceptions;
using Voca.Domain.Entities;

namespace Voca.DAL.Repositories
{
    public class ClientRepository : Repository<Client>
    {
        public IEnumerable<Client> GetByUserId(int userId)
        {
            return Context.Set<Client>().Where(c => c.UserId == userId);
        }

        public Client GetByApiKey(string apiKey)
        {
            return Context.Set<Client>().Where(c => c.ApiKey == apiKey).FirstOrDefault();
        }

        public void Update(Client entity)
        {
            Client found = this.Context.Set<Client>().Find(entity.Id);

            // Rewrite all fields (Except Id).
            found.Name = entity.Name;
            found.ApiKey = entity.ApiKey;
            found.ActiveUntil = entity.ActiveUntil;
            found.UserId = entity.UserId;

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

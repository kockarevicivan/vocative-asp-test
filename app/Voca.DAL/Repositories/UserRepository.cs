using System;
using System.Linq;
using Voca.DAL.Exceptions;
using Voca.Domain.Entities;

namespace Voca.DAL.Repositories
{
    public class UserRepository : Repository<User>
    {
        public User GetByEmail(string email)
        {
            return Context.Set<User>().Where(u => u.Email == email.ToLower()).FirstOrDefault();
        }

        public User GetByVerificationToken(string verificationToken)
        {
            return Context.Set<User>().Where(u => u.VerificationToken == verificationToken).FirstOrDefault();
        }

        public void Update(User entity)
        {
            User found = this.Context.Set<User>().Find(entity.Id);

            // Rewrite all fields (Except Id).
            found.Email = entity.Email;
            found.Password = entity.Password;
            found.DateRegistered = entity.DateRegistered;
            found.IsVerified = entity.IsVerified;
            found.VerificationToken = entity.VerificationToken;

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
using System;
using System.Collections.Generic;
using Voca.BLL.Helpers;
using Voca.BLL.Services;
using Voca.DAL.Repositories;
using Voca.Domain.Entities;

namespace Voca.BLL.Managers
{
    public class UserManager
    {
        private UserRepository _repository;
        private EmailService _emailService;


        public UserManager()
        {
            _repository = new UserRepository();
            _emailService = new EmailService();
        }


        public void Register(string email, string password)
        {
            User found = GetByEmail(email);

            if (found == null)
            {
                found = new User()
                {
                    Email = email,
                    Password = SecurityHelper.GetHash(password),
                    DateRegistered = DateTime.UtcNow,
                    IsVerified = false,
                    VerificationToken = Guid.NewGuid().ToString(),
                };

                _repository.Add(found);

                // Send verification e-mail.
                _emailService.SendVerificationEmail(email, found.VerificationToken);
            }
        }


        public User GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }


        public void Update(User entity)
        {
            _repository.Update(entity);
        }

        public void ResetPassword(string email)
        {
            User found = GetByEmail(email);

            if (found != null)
            {
                found.Password = SecurityHelper.GeneratePassword();

                _repository.Update(found);

                // Send reset password email.
                _emailService.SendResetPasswordEmail(email, found.Password);
            }
        }


        public void RemoveById(int id)
        {
            _repository.RemoveById(id);
        }


        public void Validate(string verificationToken)
        {
            User found = _repository.GetByVerificationToken(verificationToken);

            if (found != null)
            {
                found.IsVerified = true;

                _repository.Update(found);
            }
        }
    }
}

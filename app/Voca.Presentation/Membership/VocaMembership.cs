using System.Web;
using Voca.BLL.Exceptions;
using Voca.BLL.Helpers;
using Voca.BLL.Managers;
using Voca.Domain.Entities;

namespace Voca.Presentation.Membership
{
    public class VocaMembership
    {
        private UserManager _userManager;
        private User _currentUser;

        public VocaMembership()
        {
            _userManager = new UserManager();
        }

        public User CurrentUser
        {
            get
            {
                try
                {
                    if (_currentUser == null)
                    {
                        // Get current user email from the cookie.
                        string currentUserEmail = HttpContext.Current.User.Identity.Name;

                        _currentUser = _userManager.GetByEmail(currentUserEmail);
                    }
                }
                catch (BusinessException be)
                {
                    ///TODO Log error.
                }

                return _currentUser;
            }
        }

        public void Clear()
        {
            _currentUser = null;
        }

        public bool ValidateUser(string email, string password)
        {
            try
            {
                User found = _userManager.GetByEmail(email);

                if (found == null) return false;

                return SecurityHelper.GetHash(password) == found.Password;
            }
            catch (BusinessException be)
            {
                ///TODO Log error.

                return false;
            }
        }
    }
}
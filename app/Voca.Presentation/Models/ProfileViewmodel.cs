using System.Collections.Generic;
using Voca.Domain.Entities;

namespace Voca.Presentation.Models
{
    public class ProfileViewmodel
    {
        public bool UserVerified { get; set; }
        public List<Client> UserClients { get; set; }
    }
}
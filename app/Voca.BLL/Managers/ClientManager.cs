using System;
using System.Collections.Generic;
using Voca.BLL.Helpers;
using Voca.DAL.Repositories;
using Voca.Domain.Entities;

namespace Voca.BLL.Managers
{
    public class ClientManager
    {
        private ClientRepository _repository;


        public ClientManager()
        {
            _repository = new ClientRepository();
        }


        public void CreateNew(string name, DateTime activeUnil, int currentUserId)
        {
            Client newClient = new Client();
            newClient.Name = name;
            newClient.ActiveUntil = activeUnil;
            newClient.ApiKey = SecurityHelper.GenerateApiKey();
            newClient.UserId = currentUserId;

            _repository.Add(newClient);
        }


        public IEnumerable<Client> GetByUserId(int userId)
        {
            return _repository.GetByUserId(userId);
        }

        public Client GetByApiKey(string apiKey)
        {
            return _repository.GetByApiKey(apiKey);
        }

        public Client GetById(int id)
        {
            return _repository.GetById(id);
        }


        public void UpdateName(int clientId, string newName)
        {
            Client found = _repository.GetById(clientId);

            found.Name = newName;

            _repository.Update(found);
        }


        public string ResetApiKey(int clientId)
        {
            Client found = _repository.GetById(clientId);

            found.ApiKey = SecurityHelper.GenerateApiKey();

            _repository.Update(found);

            return found.ApiKey;
        }

        public void RemoveById(int id)
        {
            _repository.RemoveById(id);
        }
    }
}

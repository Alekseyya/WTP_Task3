using System.Collections.Generic;
using DAL.Repositories.Base;
using Model.DataContract;
using Model.Entities;

namespace Services
{
    public class ClientService : IClientService
    {
        //private readonly IClientRepository _clientRepo;
        //public ClientService()
        //{
        //    _clientRepo = new ClientRepository();
        //}

        public bool AddClient(ClientContract clientContract)
        {
            using (IClientRepository _clientRepo = IoC.IoC.Get<IClientRepository>())
            {
                var client = new Client()
                {
                    FirstName = clientContract.FirstName,
                    LastName = clientContract.LastName,
                    Age = clientContract.Age
                };
                if (!_clientRepo.HasClientOnDatabase(client))
                {
                    _clientRepo.Create(client);
                    return true;
                }
                else
                    return false;
                
            }
            
        }

        public void DeleteClient(int id)
        {
            using (IClientRepository _clientRepo = IoC.IoC.Get<IClientRepository>())
            {
                _clientRepo.Delete(id);
            }
        }

        public List<ClientContract> GetAllClients()
        {
            List<ClientContract> contractListClient = new List<ClientContract>();
            using (IClientRepository _clientRepo = IoC.IoC.Get<IClientRepository>())
            {
                foreach (var client in _clientRepo.GetList())
                {
                    contractListClient.Add(new ClientContract()
                    {
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Age = client.Age
                    });
                }
            }
            return contractListClient;
        }

        public ClientContract GetClientById(int id)
        {
            using (IClientRepository _clientRepo = IoC.IoC.Get<IClientRepository>())
            {
                var client = _clientRepo.GetItem(id);
                if (client != null)
                {
                    var contractClient = new ClientContract()
                    {
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Age = client.Age
                    };
                    return contractClient;
                }
                return null;
            }
        }

        public string GetMessage(string name)
        {
            return "Hello" + name;
        }

        public void UpdateClient(ClientContract clientContract)
        {
            using (IClientRepository _clientRepo = IoC.IoC.Get<IClientRepository>())
            {
                var client = new Client()
                {
                    FirstName = clientContract.FirstName,
                    LastName = clientContract.LastName,
                    Age = clientContract.Age
                };
                _clientRepo.Update(client);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL.Repositories;
using DAL.Repositories.Base;
using Model.DataContract;
using Model.Entities;

namespace Services
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ClientService" в коде и файле конфигурации.
    public class ClientService : IClientService
    {
        //private readonly IClientRepository _clientRepo;
        //public ClientService()
        //{
        //    _clientRepo = new ClientRepository();
        //}

        public void AddClient(ClientContract clientContract)
        {

            //var client = new Client()
            //{
            //    FirstName = clientContract.FirstName,
            //    LastName = clientContract.LastName,
            //    Age = clientContract.Age
            //};
            //if(!_clientRepo.HasClientOnDatabase(client))
            //    _clientRepo.Create(client);
        }

        public void DeleteClient(int id)
        {
            //_clientRepo.Delete(id);
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
            //var client = _clientRepo.GetItem(id);
            //var contractClient = new ClientContract()
            //{
            //    FirstName = client.FirstName,
            //    LastName = client.LastName,
            //    Age = client.Age
            //};
            //return contractClient;
            return null;
        }

        public string GetMessage(string name)
        {
            return "Hello" + name;
        }

        public void UpdateClient(ClientContract clientContract)
        {
            //var client = new Client()
            //{
            //    FirstName = clientContract.FirstName,
            //    LastName = clientContract.LastName,
            //    Age = clientContract.Age
            //};
            //_clientRepo.Update(client);
        }
    }
}

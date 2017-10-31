using Model.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Services
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IClientService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        string GetMessage(string name);

        [OperationContract]
        List<ClientContract> GetAllClients();

        [OperationContract]
        void AddClient(ClientContract client);

        [OperationContract]
        void DeleteClient(int id);

        [OperationContract]
        ClientContract GetClientById(int id);

        [OperationContract]
        void UpdateClient(ClientContract client);

    }
}

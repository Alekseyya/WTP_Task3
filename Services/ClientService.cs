using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Services
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ClientService" в коде и файле конфигурации.
    public class ClientService : IClientService
    {
        public string GetMessage(string name)
        {
            return "Hello" + name;
        }
    }
}

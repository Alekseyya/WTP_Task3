using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Model.DataContract;
using Model.Entities;
using WPF_Andersen.Tree;

namespace WPF_Andersen
{
    public class UpdateViewModel : PropertyChangedEvent
    {
        #region Рабочий код
        private Client _selectedClient;
        private ICommand _updateMember;

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateMember
        {
            get
            {
                if (_updateMember == null)
                {
                    Update();
                }
                return _updateMember;
            }
        }

        public void Update()
        {
            _updateMember = new RelayCommand(async obj =>
            {
                 await UpdateMemberOnDatabase();
                MessageBox.Show("Update competed");
            });
        }

        public async Task UpdateMemberOnDatabase()
        {
            await Task.Run(() =>
            {
                using (var service = new ClientService.ClientServiceClient())
                {
                    var client = SelectedClient;
                    var contractClient = new ClientContract()
                    {
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Age = client.Age
                    };
                    service.UpdateClient(contractClient);
                }
            });
        }

        public UpdateViewModel(Client client)
        {
            SelectedClient = client;
        }
        #endregion


        public UpdateViewModel()
        {
            
        }
    }
}
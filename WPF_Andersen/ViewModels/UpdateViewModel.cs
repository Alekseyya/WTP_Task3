using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Model.DataContract;
using Model.Entities;

namespace WPF_Andersen.ViewModels
{
    public class UpdateViewModel : PropertyChangedBase
    {
        #region Рабочий код
        private Client _selectedClient;
        private ICommand _updateMember;

        private string _myText = "1111111111";

        public string MyText
        {
            get
            {
                return _myText;
            }
            set
            {
                _myText = value;
                NotifyOfPropertyChange(()=> MyText);
            }
        }

        public void IncrementCount()
        {
            MyText = MyText + "2";
        }


        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                //OnPropertyChanged();
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
    //public class UpdateViewModel : PropertyChangedEvent
    //{
    //    #region Рабочий код
    //    private Client _selectedClient;
    //    private ICommand _updateMember;

    //    public Client SelectedClient
    //    {
    //        get { return _selectedClient; }
    //        set
    //        {
    //            _selectedClient = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public ICommand UpdateMember
    //    {
    //        get
    //        {
    //            if (_updateMember == null)
    //            {
    //                Update();
    //            }
    //            return _updateMember;
    //        }
    //    }

    //    public void Update()
    //    {
    //        _updateMember = new RelayCommand(async obj =>
    //        {
    //             await UpdateMemberOnDatabase();
    //            MessageBox.Show("Update competed");
    //        });
    //    }

    //    public async Task UpdateMemberOnDatabase()
    //    {
    //        await Task.Run(() =>
    //        {
    //            using (var service = new ClientService.ClientServiceClient())
    //            {
    //                var client = SelectedClient;
    //                var contractClient = new ClientContract()
    //                {
    //                    FirstName = client.FirstName,
    //                    LastName = client.LastName,
    //                    Age = client.Age
    //                };
    //                service.UpdateClient(contractClient);
    //            }
    //        });
    //    }

    //    public UpdateViewModel(Client client)
    //    {
    //        SelectedClient = client;
    //    }
    //    #endregion


    //    public UpdateViewModel()
    //    {

    //    }
    //}
}
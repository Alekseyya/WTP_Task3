using System;
using System.Collections.Generic;
using Model.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;

using Model.DataContract;
using WPF_Andersen.ViewModels;
using WPF_Andersen.Views;

namespace WPF_Andersen
{
    public class ClientViewModel : PropertyChangedBase
    {
        private Client _selectedClient;
        private bool _updateButtonClick = false;

        public CancellationTokenSource tokenSource;
        private CancellationToken token;
        private Client _newClient;

        private bool _isLoaded; //--- нужен для обновления ссылки
        private readonly IWindowManager _windowManager;

        public ClientViewModel()
        {
            ResetSourceAndToken();
            NewClient = new Client();
            NewClient.OnValidateProperty += new ValidateProperty(ValidateFirstName);

            _windowManager = new WindowManager();

        }
        public Client NewClient
        {
            get { return _newClient; }
            set
            {
                _newClient = value;
                NotifyOfPropertyChange(()=> NewClient);
            }
        }

        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                NotifyOfPropertyChange(() => Clients);
            }
        }

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                NotifyOfPropertyChange(() => SelectedClient);
            }
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                NotifyOfPropertyChange(() => IsLoaded);
            }
        }

        public void Cancel()
        {
            tokenSource.Cancel();
        }

        public async void AddClient()
        {
            var client = new Client()
            {
                FirstName = NewClient.FirstName,
                LastName = NewClient.LastName,
                Age = NewClient.Age
            };
            //_clientTest.OnValidateProperty();
            await AddMemberOnDatabase(client);
            await Load();
        }
        

        public async void DeleteClient()
        {
            var client = SelectedClient;
            if (client != null)
            {
                await DeleteMemberOnDatabase(client);
            }
        }
        
        public async void Update()
        {
            if (!_updateButtonClick)
            {
                _updateButtonClick = true;
                await Load();
            }
            else
            {
                tokenSource.Cancel();
                if (tokenSource.IsCancellationRequested)
                    ResetSourceAndToken();
                await Load();
            }
        }

        public string ValidateFirstName(string propertyName)
        {
            //if (string.IsNullOrEmpty(this.FirstName))
            //    return "LastName can't be empty.";

            Regex regex = new Regex(@"^[A-Z][a-z]{3,19}$");
            Match match = regex.Match(propertyName);
            if (match.Success)
            {
                return "Success";
            }
            return "First letter uppercase and legth 3 - 19";
        }

        public void ResetSourceAndToken()
        {
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }
        

        public async Task AddMemberOnDatabase(Client client)
        {
            await Task.Run(() =>
            {
                bool flag = false;
                using (var service = new ClientService.ClientServiceClient())
                {
                    var clientContract = new ClientContract()
                    {
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Age = client.Age
                    };
                    flag = service.AddClient(clientContract);
                }
                if (!flag)
                    MessageBox.Show("Такой пользователь уже существует");
            });
        }
        

        public async Task DeleteMemberOnDatabase(Client client)
        {
            await Task.Run(() =>
            {
                var aa = client;
                using (var service = new ClientService.ClientServiceClient())
                {

                    service.DeleteClient(client.Id);
                }
            });
            Clients.Remove(client);
        }

        public async Task Load()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await LoadAsync();
            IsLoaded = true;
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
        }

        private async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                if (token.IsCancellationRequested)
                {
                    MessageBox.Show("Операция отменена");
                    return;
                }
                var listClients = new List<Client>();
                using (var service = new ClientService.ClientServiceClient())
                {
                    var contractClients = service.GetAllClients();
                    foreach (var client in contractClients)
                    {
                        listClients.Add(new Client()
                        {
                            FirstName = client.FirstName,
                            LastName = client.LastName,
                            Age = client.Age
                        });
                    }
                }
                Clients = new ObservableCollection<Client>(listClients);
            }, token);
        }

        public void Open(object client)
        {
            _windowManager.ShowDialog(new UpdateViewModel((Client) client));
        }

    }
}

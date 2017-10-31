using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DAL.Repositories.Base;
using Model.Entities;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Dapper;

namespace DAL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        
        private readonly IDbConnection db;

        public ClientRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
            db = new SqlConnection(connectionString);
        }
        public void Create(Client client)
        {
            var sqlQuery = "INSERT INTO Clients (FirstName, LastName, Age) VALUES(@FirstName, @LastName, @Age);" +
                           "SELECT CAST(SCOPE_IDENTITY() as int)";
            int? clientId = db.Query<int>(sqlQuery, client).FirstOrDefault();
            client.Id = (int)clientId;

        }

        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Clients WHERE Id = @id";
            db.Execute(sqlQuery, new { id });
        }

        public bool HasClientOnDatabase(Client client)
        {
            var findClient = db.Query<Client>("Select * From Clients WHERE " +
                                  "FirstName = @FirstName And LastName = @LastName " +
                                  "and Age =@Age", new { FirstName = client.FirstName, LastName = client.LastName, Age = client.Age });
            if(findClient.Count()==0)
                return false;

            return true;
        }

        public Client GetItem(int id)
        {
            Client client = db.Query<Client>("SELECT * FROM Clients WHERE Id = @id", new { id }).FirstOrDefault();
            if (client != null)
                return client;
            return null;
        }

        public IEnumerable<Client> GetList()
        {
            List<Client> clients = clients = db.Query<Client>("Select * from Clients").ToList(); 
            return clients;
        }
        
        public void Update(Client client)
        {
           var sqlQuery = "UPDATE Clients SET FirstName = @FirstName, LastName = @Lastname, Age = @Age WHERE Id = @Id";
           db.Execute(sqlQuery, client);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

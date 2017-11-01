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
        
        //private readonly IDbConnection db;
        private readonly Singleton db;
        public ClientRepository()
        {
            db = Singleton.Instance();
            //string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ToString();
            //db = new SqlConnection(connectionString);
        }
        public void Create(Client client)
        {
            using (var connection = db.GetOpenConnection())
            {
                var sqlQuery = "INSERT INTO Clients (FirstName, LastName, Age) VALUES(@FirstName, @LastName, @Age);" +
                               "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? clientId = connection.Query<int>(sqlQuery, client).FirstOrDefault();
                client.Id = (int)clientId;
            }
        }

        public void Delete(int id)
        {
            using (var conn = db.GetOpenConnection())
            {
                var sqlQuery = "DELETE FROM Clients WHERE Id = @id";
                conn.Execute(sqlQuery, new { id });
            }
        }

        public bool HasClientOnDatabase(Client client)
        {
            using (var conn = db.GetOpenConnection())
            {
                var findClient = conn.Query<Client>("Select * From Clients WHERE " +
                                      "FirstName = @FirstName And LastName = @LastName " +
                                      "and Age =@Age", new { FirstName = client.FirstName, LastName = client.LastName, Age = client.Age });
                if (findClient.Count() == 0)
                    return false;
                return true;
            }
        }

        public Client GetItem(int id)
        {
            using (var conn = db.GetOpenConnection())
            {
                Client client = conn.Query<Client>("SELECT * FROM Clients WHERE Id = @id", new { id }).FirstOrDefault();
                if (client != null)
                    return client;
                return null;
            }
        }

        public IEnumerable<Client> GetList()
        {
            List<Client> clients = new List<Client>();
            using (var conn = db.GetOpenConnection())
            {
                clients = conn.Query<Client>("Select * from Clients").ToList();
            }
            return clients;
        }

        public void Update(Client client)
        {
            using (var conn = db.GetOpenConnection())
            {
                var sqlQuery = "UPDATE Clients SET FirstName = @FirstName, LastName = @Lastname, Age = @Age WHERE Id = @Id";
                conn.Execute(sqlQuery, client);
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //db.Dispose();
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

using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly ApplicationDbContext _db;

        public ClientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Client client)
        {
            var dbObject = _db.Clients.FirstOrDefault(x => x.ID == client.ID);
            if (dbObject != null)
            {
                dbObject.Name = client.Name;
                dbObject.Surname = client.Surname;
                dbObject.Adress = client.Adress;
                dbObject.Phone = client.Phone;
                dbObject.Debt = client.Debt;
                dbObject.HealthInsuranceID = client.HealthInsuranceID;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Client client)
        {
            var dbObject = _db.Clients.FirstOrDefault(
                x => x.Name.ToLower() == client.Name.ToLower() && x.Surname.ToLower() == client.Surname.ToLower() && 
                x.Phone.ToLower() == client.Phone.ToLower() && x.ID != client.ID);

            if (dbObject == null) return false;
            return true;
        }

        public void ChangeState(long id)
        {
            var dbObject = _db.Clients.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.IsActive = !dbObject.IsActive;
                _db.SaveChanges();
            }
        }
    }
}
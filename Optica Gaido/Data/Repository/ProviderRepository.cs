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
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        private readonly ApplicationDbContext _db;

        public ProviderRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public void Update(Provider provider)
        {
            var dbObject = _db.Providers.FirstOrDefault(x => x.ID == provider.ID);
            if (dbObject != null)
            {
                dbObject.Name = provider.Name;
                _db.SaveChanges();
            }
        }

        public void SoftDelete(long id)
        {
            var dbObject = _db.Providers.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Provider provider)
        {
            var dbObject = _db.Providers.FirstOrDefault(x => x.Name.ToLower() == provider.Name.ToLower() && x.DeletedAt == null && x.ID != provider.ID);
            return dbObject != null;
        }
    }
}
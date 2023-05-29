using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Optica_Gaido.Data.Repository.IRepository;

namespace Optica_Gaido.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {
        private readonly ApplicationDbContext _db;

        public WorkContainer(ApplicationDbContext db)
        {
            _db = db;
            Brand = new BrandRepository(_db);
        }

        public IBrandRepository Brand { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
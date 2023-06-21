using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class DebtRepository : Repository<Debt>, IDebtRepository
    {
        private readonly ApplicationDbContext _db;

        public DebtRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public IEnumerable<Debt> GetProviderDebts(long id)
        {
            return _db.Debts.Where(x => x.ProviderID == id && x.DeletedAt == null)
            .Include(x => x.DebtPayment)
            .OrderBy(x => x.CreatedAt)
            .ThenByDescending(x => x.Price)
            .ToList();
        }

        public void SoftDelete(long id)
        {
            var dbObject = _db.Debts.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }

        public Debt GetOneWithProperties(long id, string properties)
        {
            IQueryable<Debt> query = dbSet;
            // Include properties separados por coma
            if (properties != null)
            {
                foreach (var prop in properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop.Trim());
                }
            }
            return query.Where(x => x.ID == id).FirstOrDefault();
        }
    }
}
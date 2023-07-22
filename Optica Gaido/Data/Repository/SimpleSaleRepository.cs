using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class SimpleSaleRepository : Repository<SimpleSale>, ISimpleSaleRepository
    {
        private readonly ApplicationDbContext _db;

        public SimpleSaleRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public void SoftDelete(long id)
        {
            _db.Database.BeginTransaction();
            try
            {
                SimpleSale dbObject = _db.SimpleSales.Include(x => x.PaymentMethods).Include(x => x.Products).Where(x => x.ID == id).FirstOrDefault();
                if (dbObject != null)
                {

                    dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                    foreach (var product in dbObject.Products)
                    {
                        product.DeletedAt = DateTime.UtcNow.AddHours(-3);
                    }
                    _db.SaveChanges();
                    _db.Database.CommitTransaction();
                } else
                {
                    _db.Database.RollbackTransaction();
                    throw new Exception("No se encontró la venta");
                }
            }
            catch (Exception)
            {
                _db.Database.RollbackTransaction();
                throw;
            }
        }

        public IEnumerable<SelectListItem> GetYears()
        {
            var years = this.GetAll()
            .Select(sale => sale.CreatedAt.Year)
            .Distinct()
            .OrderByDescending(year => year)
            .ToList();

            return years.Select(year => new SelectListItem
            {
                Text = year.ToString(),
                Value = year.ToString(),
                Selected = (year == DateTime.UtcNow.AddHours(-3).Year)
            });
        }
    }
}
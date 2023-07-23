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
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        private readonly ApplicationDbContext _db;

        public SaleRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public void Update(Sale sale)
        {
            var dbObject = _db.Sales.FirstOrDefault(x => x.ID == sale.ID);
            if (dbObject != null)
            {
                dbObject.Price = sale.Price;
                dbObject.Deposit = sale.Deposit;
                dbObject.MovieHeight = sale.MovieHeight;
                dbObject.Dip = sale.Dip;
                dbObject.Observation = sale.Observation;
                dbObject.IsAr = sale.IsAr;
                dbObject.GlassTypeID = sale.GlassTypeID;
                dbObject.GlassColorID = sale.GlassColorID;
                dbObject.GlassFocusTypeID = sale.GlassFocusTypeID;
                dbObject.DoctorID = sale.DoctorID;
                dbObject.SellerID = sale.SellerID;
                //dbObject.FrameID = sale.FrameID;
                dbObject.DeliveryDate = sale.DeliveryDate;
                _db.SaveChanges();
            }
        }

        public void SoftDelete(long id)
        {
            _db.Database.BeginTransaction();
            try
            {
                Sale dbObject = _db.Sales.Include(x => x.SalePaymentMethods).Include(x => x.GlassFormats).Where(x => x.ID == id).FirstOrDefault();
                if (dbObject != null)
                {

                    dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                    foreach (var format in dbObject.GlassFormats)
                    {
                        format.DeletedAt = DateTime.UtcNow.AddHours(-3);
                    }
                    _db.SaveChanges();
                    _db.Database.CommitTransaction();
                } else
                {
                    _db.Database.RollbackTransaction();
                    throw new Exception("No se encontr� la venta");
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
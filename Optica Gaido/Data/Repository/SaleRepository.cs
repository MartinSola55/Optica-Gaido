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
                dbObject.DoctorID = sale.DoctorID;
                dbObject.SellerID = sale.SellerID;
                dbObject.ClientID = sale.ClientID;
                dbObject.FrameID = sale.FrameID;
                dbObject.DeliveryDate = sale.DeliveryDate;
                _db.SaveChanges();
            }
        }

        public void SoftDelete(long id)
        {
            var dbObject = _db.Sales.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }
    }
}
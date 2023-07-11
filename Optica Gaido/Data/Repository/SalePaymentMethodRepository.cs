using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class SalePaymentMethodRepository : Repository<SalePaymentMethod>, ISalePaymentMethodRepository
    {
        private readonly ApplicationDbContext _db;

        public SalePaymentMethodRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public void Update(SalePaymentMethod method)
        {
            var dbObject = _db.SalePaymentMethods.FirstOrDefault(x => x.SaleID == method.SaleID && x.PaymentMethodID == method.PaymentMethodID);
            if (dbObject != null)
            {
                dbObject.Amount = method.Amount;
                _db.SaveChanges();
            }
        }

        public SalePaymentMethod GetOne(long sale_id, long pm_id)
        {
            return _db.SalePaymentMethods.Where(x => x.SaleID == sale_id && x.PaymentMethodID == pm_id).First();
        }
    }
}
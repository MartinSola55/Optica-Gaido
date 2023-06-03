using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
    {
        private readonly ApplicationDbContext _db;

        public PaymentMethodRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> methods = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione un método", Disabled = true }
            };
            return methods.Concat(_db.PaymentMethods.Where(x => x.IsActive).OrderBy(x => x.Name).Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.ID.ToString(),
            }));
        }

        public void Update(PaymentMethod method)
        {
            var dbObject = _db.PaymentMethods.FirstOrDefault(x => x.ID == method.ID);
            if (dbObject != null)
            {
                dbObject.Name = method.Name;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(PaymentMethod method)
        {
            var dbObject = _db.PaymentMethods.FirstOrDefault(x => x.Name.ToLower() == method.Name.ToLower() && x.ID != method.ID);
            return dbObject != null;
        }

        public void ChangeState(short id)
        {
            var dbObject = _db.PaymentMethods.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.IsActive = !dbObject.IsActive;
                _db.SaveChanges();
            }
        }
    }
}
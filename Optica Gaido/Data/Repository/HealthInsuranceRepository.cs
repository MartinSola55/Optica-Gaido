using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class HealthInsuranceRepository : Repository<HealthInsurance>, IHealthInsuranceRepository
    {
        private readonly ApplicationDbContext _db;

        public HealthInsuranceRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetDropDownHealthInsurances()
        {
            return _db.HealthInsurances.Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.ID.ToString(),
            });
        }

        public void Update(HealthInsurance healthInsurance)
        {
            var dbObject = _db.Brands.FirstOrDefault(x => x.ID == healthInsurance.ID);
            dbObject.Name = healthInsurance.Name;
            _db.SaveChanges();
        }
    }
}
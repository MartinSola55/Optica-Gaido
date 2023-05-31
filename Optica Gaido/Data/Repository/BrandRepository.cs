using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _db;

        public BrandRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> brands = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione una marca", Disabled = true }
            };
            return brands.Concat(_db.Brands.Where(x => x.IsActive == true).OrderBy(x => x.Name).Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.ID.ToString(),
            }));
        }

        public void Update(Brand brand)
        {
            var dbObject = _db.Brands.FirstOrDefault(x => x.ID == brand.ID);
            if (dbObject != null)
            {
                dbObject.Name = brand.Name;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Brand brand)
        {
            var dbObject = _db.HealthInsurances.FirstOrDefault(x => string.Equals(x.Name, brand.Name, StringComparison.OrdinalIgnoreCase) && x.ID != brand.ID);
            return dbObject != null;
        }

        public void ChangeState(long id)
        {
            var dbObject = _db.Brands.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.IsActive = !dbObject.IsActive;
                _db.SaveChanges();
            }
        }
    }
}
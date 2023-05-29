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
        public IEnumerable<SelectListItem> GetDropDownBrands()
        {
            return _db.Brands.Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.ID.ToString(),
            });
        }

        public void Update(Brand brand)
        {
            var dbObject = _db.Brands.FirstOrDefault(x => x.ID == brand.ID);
            dbObject.Name = brand.Name;
            _db.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        private readonly ApplicationDbContext _db;

        public MaterialRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetDropDownList()
        {
            return _db.Materials.Where(x => x.IsActive == true).OrderBy(x => x.Description).Select(i => new SelectListItem() {
                Text = i.Description,
                Value = i.ID.ToString(),
            });
        }

        public void Update(Material material)
        {
            var dbObject = _db.Materials.FirstOrDefault(x => x.ID == material.ID);
            if (dbObject != null)
            {
                dbObject.Description = material.Description;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Material material)
        {
            var dbObject = _db.Materials.FirstOrDefault(x => x.Description.ToLower() == material.Description.ToLower() && x.ID != material.ID);
            if (dbObject == null) return false;
            return true;
        }

        public void ChangeState(long id)
        {
            var dbObject = _db.Materials.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.IsActive = !dbObject.IsActive;
                _db.SaveChanges();
            }
        }
    }
}
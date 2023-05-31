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
            IEnumerable<SelectListItem> materials = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione un material", Disabled = true }
            };
            return materials.Concat(_db.Materials.Where(x => x.IsActive).OrderBy(x => x.Description).Select(i => new SelectListItem() {
                Text = i.Description,
                Value = i.ID.ToString(),
            }));
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
            var dbObject = _db.Materials.FirstOrDefault(x => string.Equals(x.Description, material.Description, StringComparison.OrdinalIgnoreCase) && x.ID != material.ID);
            return dbObject != null;
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
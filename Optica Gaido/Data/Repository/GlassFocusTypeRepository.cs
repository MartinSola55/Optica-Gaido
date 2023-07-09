using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class GlassFocusTypeRepository : Repository<GlassFocusType>, IGlassFocusTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public GlassFocusTypeRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> focuses = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione un foco", Disabled = true }
            };
            return focuses.Concat(_db.GlassFocusTypes.OrderBy(x => x.Name).Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.ID.ToString(),
            }));
        }
    }
}
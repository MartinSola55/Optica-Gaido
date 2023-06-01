using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class GlassColorRepository : Repository<GlassColor>, IGlassColorRepository
    {
        private readonly ApplicationDbContext _db;

        public GlassColorRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> colors = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione un color", Disabled = true }
            };
            return colors.Concat(_db.GlassColors.OrderBy(x => x.Name).Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.ID.ToString(),
            }));
        }
    }
}
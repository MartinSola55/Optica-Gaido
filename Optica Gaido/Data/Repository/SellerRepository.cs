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
    public class SellerRepository : Repository<Seller>, ISellerRepository
    {
        private readonly ApplicationDbContext _db;

        public SellerRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> sellers = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione un vendedor", Disabled = true }
            };
            return sellers.Concat(_db.Sellers.Where(x => x.IsActive).OrderBy(x => x.Surname).ThenBy(x => x.Name).Select(i => new SelectListItem()
            {
                Text = i.Surname + ", " + i.Name,
                Value = i.ID.ToString(),
            }));
        }
    }
}
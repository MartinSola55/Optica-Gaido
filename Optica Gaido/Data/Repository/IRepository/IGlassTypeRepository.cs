using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IGlassTypeRepository : IRepository<GlassType>
    {
        IEnumerable<SelectListItem> GetDropDownList();
    }
}
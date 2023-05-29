using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        IEnumerable<SelectListItem> GetDropDownBrands();

        void Update(Brand brand);
    }
}
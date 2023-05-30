using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IMaterialRepository : IRepository<Material>
    {
        IEnumerable<SelectListItem> GetDropDownList();

        void Update(Material material);
        bool IsDuplicated(Material material);
        void ChangeState(long id);
    }
}
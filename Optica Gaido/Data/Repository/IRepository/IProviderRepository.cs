using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IProviderRepository : IRepository<Provider>
    {
        IEnumerable<SelectListItem> GetDropDownList();

        void Update(Provider provider);
        void SoftDelete(long id);
        bool IsDuplicated(Provider provider);
    }
}
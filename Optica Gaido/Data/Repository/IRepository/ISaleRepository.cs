using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface ISaleRepository : IRepository<Sale>
    {
        void Update(Sale sale);
        void SoftDelete(long id);
    }
}
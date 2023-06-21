using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IDebtRepository : IRepository<Debt>
    {
        IEnumerable<Debt> GetProviderDebts(long id);
        Debt GetOneWithProperties(long id, string properties);
        void SoftDelete(long id);
    }
}
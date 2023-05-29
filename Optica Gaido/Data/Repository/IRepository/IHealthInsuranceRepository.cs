using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IHealthInsuranceRepository : IRepository<HealthInsurance>
    {
        IEnumerable<SelectListItem> GetDropDownHealthInsurances();

        void Update(HealthInsurance healthInsurance);
    }
}
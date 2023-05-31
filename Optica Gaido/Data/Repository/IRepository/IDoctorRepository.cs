using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        IEnumerable<SelectListItem> GetDropDownList();

        void Update(Doctor doctor);
        bool IsDuplicated(Doctor doctor);
        void ChangeState(long id);
    }
}
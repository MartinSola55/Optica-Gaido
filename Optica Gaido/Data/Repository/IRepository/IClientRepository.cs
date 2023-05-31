using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IClientRepository : IRepository<Client>
    {
        void Update(Client client);
        bool IsDuplicated(Client client);
        void ChangeState(long id);
    }
}
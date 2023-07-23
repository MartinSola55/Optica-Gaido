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
        Client GetOneWithProperties(long id, string properties);
        bool IsDuplicated(Client client);
        void ChangeState(long id);
        void SoftDelete(long id);
        string GetLastSale(long clientID);
    }
}
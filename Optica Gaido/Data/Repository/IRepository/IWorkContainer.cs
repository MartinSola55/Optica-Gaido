using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IWorkContainer : IDisposable
    {
        // Agregar los repositorios
        IBrandRepository Brand { get; }

        void Save();
    }
}
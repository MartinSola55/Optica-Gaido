using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class GlassFormatRepository : Repository<GlassFormat>, IGlassFormatRepository
    {
        private readonly ApplicationDbContext _db;

        public GlassFormatRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class FrameRepository : Repository<Frame>, IFrameRepository
    {
        private readonly ApplicationDbContext _db;

        public FrameRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public void Update(Frame frame)
        {
            var dbObject = _db.Frames.FirstOrDefault(x => x.ID == frame.ID);
            if (dbObject != null)
            {
                dbObject.Model = frame.Model;
                dbObject.BrandID = frame.BrandID;
                dbObject.MaterialID = frame.MaterialID;
                _db.SaveChanges();
            }
        }

        public void SoftDelete(long id)
        {
            var dbObject = _db.Frames.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Frame frame)
        {
            var dbObject = _db.Frames.FirstOrDefault(x => string.Equals(x.Model, frame.Model, StringComparison.OrdinalIgnoreCase) && x.ID != frame.ID);
            return dbObject != null;
        }
    }
}
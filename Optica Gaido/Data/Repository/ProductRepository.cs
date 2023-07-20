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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var dbObject = _db.Products.FirstOrDefault(x => x.ID == product.ID);
            if (dbObject != null)
            {
                dbObject.Name = product.Name;
                dbObject.Price = product.Price;
                dbObject.Stock = product.Stock;
                dbObject.CreatedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }
        public void SoftDelete(long id)
        {
            var dbObject = _db.Products.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }
    }
}
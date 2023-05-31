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
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        private readonly ApplicationDbContext _db;

        public ExpenseRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public void Update(Expense expense)
        {
            var dbObject = _db.Expenses.FirstOrDefault(x => x.ID == expense.ID);
            if (dbObject != null)
            {
                dbObject.Amount = expense.Amount;
                dbObject.Description = expense.Description;
                dbObject.CreatedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }
    }
}
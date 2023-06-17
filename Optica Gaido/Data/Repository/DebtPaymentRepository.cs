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
    public class DebtPaymentRepository : Repository<DebtPayment>, IDebtPaymentRepository
    {
        private readonly ApplicationDbContext _db;

        public DebtPaymentRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
    }
}
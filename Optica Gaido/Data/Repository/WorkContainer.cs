using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Optica_Gaido.Data.Repository.IRepository;

namespace Optica_Gaido.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {
        private readonly ApplicationDbContext _db;

        public WorkContainer(ApplicationDbContext db)
        {
            _db = db;
            Brand = new BrandRepository(_db);
            HealthInsurance = new HealthInsuranceRepository(_db);
            Material = new MaterialRepository(_db);
            Provider = new ProviderRepository(_db);
            Frame = new FrameRepository(_db);
            Expense = new ExpenseRepository(_db);
            Doctor = new DoctorRepository(_db);
            Client = new ClientRepository(_db);
            PaymentMethod = new PaymentMethodRepository(_db);
            SalePaymentMethod = new SalePaymentMethodRepository(_db);
            Seller = new SellerRepository(_db);
            GlassColor = new GlassColorRepository(_db);
            GlassType = new GlassTypeRepository(_db);
            GlassFocusType = new GlassFocusTypeRepository(_db);
            GlassFormat = new GlassFormatRepository(_db);
            Sale = new SaleRepository(_db);
            Debt = new DebtRepository(_db);
            DebtPayment = new DebtPaymentRepository(_db);
            Product = new ProductRepository(_db);
            SimpleSale = new SimpleSaleRepository(_db);
        }

        public IBrandRepository Brand { get; private set; }
        public IHealthInsuranceRepository HealthInsurance { get; private set; }

        public IMaterialRepository Material { get; private set; }
        public IProviderRepository Provider { get; private set; }
        public IFrameRepository Frame { get; private set; }
        public IExpenseRepository Expense { get; private set; }
        public IDoctorRepository Doctor { get; private set; }
        public IClientRepository Client { get; private set; }
        public IPaymentMethodRepository PaymentMethod { get; private set; }
        public ISalePaymentMethodRepository SalePaymentMethod { get; private set; }
        public ISellerRepository Seller { get; private set; }
        public IGlassColorRepository GlassColor { get; private set; }
        public IGlassTypeRepository GlassType { get; private set; }
        public IGlassFocusTypeRepository GlassFocusType { get; private set; }
        public IGlassFormatRepository GlassFormat { get; private set; }
        public ISaleRepository Sale { get; private set; }
        public IDebtRepository Debt { get; private set; }
        public IDebtPaymentRepository DebtPayment { get; private set; }
        public IProductRepository Product { get; private set; }
        public ISimpleSaleRepository SimpleSale { get; private set; }

        public void BeginTransaction()
        {
            _db.Database.BeginTransaction();
        }

        public void Commit()
        {
            _db.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _db.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
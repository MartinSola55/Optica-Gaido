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
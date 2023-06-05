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
        IHealthInsuranceRepository HealthInsurance { get; }
        IMaterialRepository Material{ get; }
        IProviderRepository Provider { get; }
        IFrameRepository Frame { get; }
        IExpenseRepository Expense { get; }
        IDoctorRepository Doctor { get; }
        IClientRepository Client { get; }
        IPaymentMethodRepository PaymentMethod { get; }
        ISalePaymentMethodRepository SalePaymentMethod { get; }
        ISellerRepository Seller { get; }
        IGlassColorRepository GlassColor { get; }
        IGlassTypeRepository GlassType { get; }
        IGlassFormatRepository GlassFormat { get; }
        ISaleRepository Sale { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
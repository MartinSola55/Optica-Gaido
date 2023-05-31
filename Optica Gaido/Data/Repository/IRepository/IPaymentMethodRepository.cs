using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository.IRepository
{
    public interface IPaymentMethodRepository : IRepository<PaymentMethod>
    {
        IEnumerable<SelectListItem> GetDropDownList();

        void Update(PaymentMethod paymentMethod);
        bool IsDuplicated(PaymentMethod paymentMethod);
        void ChangeState(short id);
    }
}

using ServicesLayer;
using ServicesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICurrencyService Currencies { get; set; }
        IExchangeHistoryService ExchangeHistories { get; set; }
        IIdentityService Identites { get; set; }
        int Complete();
    }
}

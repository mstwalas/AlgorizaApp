using CoreLayer;
using RepositryLayer;
using ServicesLayer;
using ServicesLayer.Interfaces;
using ServicesLayer.Service;
using UnitOfWorkLayer.Interfaces;

namespace UnitOfWorkLayer
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext AppDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Currencies = new CurrencyService(AppDbContext);
            ExchangeHistories = new ExchangeHistoryService(AppDbContext);
            Identites = new IdentityService(AppDbContext);
        }
        public ICurrencyService Currencies { get; set; }
        public IExchangeHistoryService ExchangeHistories { get; set; }
        public IIdentityService Identites { get; set; }

        public  int Complete()
        {
            return  AppDbContext.SaveChanges();
        }

        public void Dispose()
        {
            AppDbContext.Dispose();
        }
    }
}
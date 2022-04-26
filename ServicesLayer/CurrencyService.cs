using CoreLayer;
using CoreLayer.Model;
using RepositryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class CurrencyService : Repositry<Currency>, ICurrencyService
    {
        public CurrencyService(AppDbContext appDbContext) 
            : base(appDbContext)
        {
            
        }

        public AppDbContext AppDbContext
        {
            get { return Context as AppDbContext; }
        }
        public void AddCurrency(Currency currency)
        {
            var oldcurrency= GetCurrencyByName(currency.Name);
            if (oldcurrency != null)
            {
                oldcurrency.IsActive = true;
            }
            else
            {
                currency.Id = 0;
                AppDbContext.Currencies.Add(currency);
            }
        }
        public IEnumerable<Currency> GetAllCurrencies()
        {
            return AppDbContext.Currencies.Where(e=>e.IsActive==true);
        }

        public Currency GetCurrencyByName(string Name)
        {
            return AppDbContext.Currencies.FirstOrDefault(e=>e.Name==Name);
        }
    }
}

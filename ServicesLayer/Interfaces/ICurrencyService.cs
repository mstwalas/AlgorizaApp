using CoreLayer.Model;
using RepositryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public interface ICurrencyService:IRepositry<Currency>
    {
        void AddCurrency(Currency currency);
        IEnumerable<Currency> GetAllCurrencies();
        Currency GetCurrencyByName(string Name);
    }
}

using CoreLayer.Model;
using RepositryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public interface IExchangeHistoryService: IRepositry<ExchangeHistory>
    {

        void AddExchange(ExchangeHistory exchangeHistory);
        IEnumerable<ExchangeHistory> CurrenciesImprovingRateByDate(DateTime Date);
        IEnumerable<ExchangeHistory> GetTheLastRateOfAllCurrencies();
        public double GetTheLastRateOfCurrency(Currency currency);
        List<Currency> GetHighestNCurrencies(int Num);
        List<Currency> GetLowestNCurrencies(int Num);
        List<Currency> GetMostNImprovedCurrenciesByDate(int Num, DateTime Date);
        List<Currency> GetLeastNImprovedCurrenciesByDate(int Num, DateTime Date);

    }
}

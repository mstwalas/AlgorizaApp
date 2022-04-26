using CoreLayer.Model;

using Microsoft.EntityFrameworkCore;
using RepositryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Service
{
    public class ExchangeHistoryService : Repositry<ExchangeHistory>, IExchangeHistoryService
    {
        //CurrencyService : Repositry<Currency>, ICurrencyService
        public ExchangeHistoryService(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public AppDbContext AppDbContext
        {
            get { return Context as AppDbContext; }
        }

        public List<Currency> GetNamesOfCurrencies(IEnumerable<ExchangeHistory> exchangeHistories)
        {
            List<Currency> names = new List<Currency>();
            foreach (var exchangeHistory in exchangeHistories)
            {
                names.Add(AppDbContext.Currencies.FirstOrDefault(e => e.Id == exchangeHistory.Currency.Id));
            }
            return names;
        }
        public IEnumerable<ExchangeHistory> CurrenciesImprovingRateByDate(DateTime Date)
        {
            List<ExchangeHistory> CurrenciesWithAvgRates = new List<ExchangeHistory>();
            var AllCurrencies = AppDbContext.Currencies.Where(e => e.IsActive == true).ToList();
            foreach (var currency in AllCurrencies)
            {
                var ExchangeHistoriesForCurrency = AppDbContext.ExchangeHistory.
                    Where(e => e.Currency.Id == currency.Id && e.ExchangeDate <= Date).OrderBy(e=>e.ExchangeDate);
                if (ExchangeHistoriesForCurrency != null&&ExchangeHistoriesForCurrency.Count()>1)
                {
                    var FirstCurrencyRate = ExchangeHistoriesForCurrency.First().Rate;

                    var LastCurrencyRate = ExchangeHistoriesForCurrency.Last().Rate;
                    var CurrencyImprovingValue = (LastCurrencyRate - FirstCurrencyRate) / FirstCurrencyRate;

                    CurrenciesWithAvgRates.Add(new ExchangeHistory { Currency = currency, Rate = CurrencyImprovingValue });
                }
                else
                {
                    CurrenciesWithAvgRates.Add(new ExchangeHistory { Currency = currency, Rate = 0 });
                }
                
                
            }
            return CurrenciesWithAvgRates;
        }
        public IEnumerable<ExchangeHistory> GetTheLastRateOfAllCurrencies()
        {
            List<ExchangeHistory> Currencieswithrate = new List<ExchangeHistory>();

            var AllCurrencies = AppDbContext.Currencies.Where(e => e.IsActive == true).ToList();
            foreach (var currency in AllCurrencies)
            {
                var ExchangesForThisCurrency = (AppDbContext.ExchangeHistory.Where(e => e.Currency.Id == currency.Id));
                if (ExchangesForThisCurrency.Count() > 0)
                {
                    var LastExchangeHistory = ExchangesForThisCurrency.OrderByDescending(e => e.ExchangeDate).First();
                    Currencieswithrate.Add(LastExchangeHistory);
                }
            }
            return Currencieswithrate;
        }
        public double GetTheLastRateOfCurrency(Currency currency)
        {
            var Currency = currency;

            var LastExchangeOfThisCurrency = (AppDbContext.ExchangeHistory.
                Where(e => e.Currency.Id == currency.Id)).OrderByDescending(e => e.ExchangeDate).FirstOrDefault();
            if(LastExchangeOfThisCurrency != null)
            {
                return LastExchangeOfThisCurrency.Rate;
            }
            else
            {
                return 0;
            }
        }


        public void AddExchange(ExchangeHistory exchangeHistory)
        {

            AppDbContext.ExchangeHistory.Add(exchangeHistory);

        }

        public List<Currency> GetHighestNCurrencies(int Num)
        {
            var Currencieswithrate = GetTheLastRateOfAllCurrencies();
            if (Currencieswithrate.Count() < Num)
            {
                return GetNamesOfCurrencies(Currencieswithrate.OrderByDescending(e => e.Rate));
            }
            else
            {
                return GetNamesOfCurrencies( Currencieswithrate.OrderByDescending(e => e.Rate).Take(Num));
            }
        }

        public List<Currency> GetLowestNCurrencies(int Num)
        {
            var Currencieswithrate = GetTheLastRateOfAllCurrencies();
            if (Currencieswithrate.Count() < Num)
            {
                return GetNamesOfCurrencies( Currencieswithrate.OrderBy(e => e.Rate));
            }
            else
            {
                return GetNamesOfCurrencies( Currencieswithrate.OrderBy(e => e.Rate).Take(Num));
            }
        }
        
        public List<Currency> GetLeastNImprovedCurrenciesByDate(int Num, DateTime Date)
        {
            var CurrenciesImprovingRate = CurrenciesImprovingRateByDate(Date);
            if (Num > CurrenciesImprovingRate.Count())
            {
                return GetNamesOfCurrencies(CurrenciesImprovingRate.OrderBy(e => e.Rate)); 
            }
            else
            {
                return GetNamesOfCurrencies(CurrenciesImprovingRate.OrderBy(e => e.Rate).Take(Num));
            }
        }
        
        public List<Currency> GetMostNImprovedCurrenciesByDate(int Num,DateTime Date)
        {
            var CurrenciesImprovingRate= CurrenciesImprovingRateByDate(Date);
            if (Num > CurrenciesImprovingRate.Count())
            {
                return GetNamesOfCurrencies(CurrenciesImprovingRate.OrderByDescending(e => e.Rate));
            }
            else
            {
                return GetNamesOfCurrencies(CurrenciesImprovingRate.OrderByDescending(e => e.Rate).Take(Num));
            }
        }
        
    }
}

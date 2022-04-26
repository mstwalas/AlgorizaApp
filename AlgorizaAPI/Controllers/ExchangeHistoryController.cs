using CoreLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositryLayer;
using UnitOfWorkLayer;

namespace AlgorizaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeHistoryController : ControllerBase
    {
        UnitOfWork unitOfWork;
        public ExchangeHistoryController()
        {
             unitOfWork= new UnitOfWork(new AppDbContext());
        }

        [HttpPost("AddExchangeHistory")]
        public ActionResult AddExchangeHistory(int CurrencyId, double rate)
        {
            ExchangeHistory exchangeHistory = new ExchangeHistory();
            exchangeHistory.Currency = unitOfWork.Currencies.Get(CurrencyId);
            exchangeHistory.Rate = rate;
            exchangeHistory.ExchangeDate = DateTime.Now;
            unitOfWork.ExchangeHistories.AddExchange(exchangeHistory);
            unitOfWork.Complete();
            return Ok();
        }

        [HttpGet("GetHighestNCurrencies")]
        public List<Currency> GetHighestNCurrencies(int Num)
        {
            return unitOfWork.ExchangeHistories.GetHighestNCurrencies(Num);
            
        }
        [HttpGet("GetLowestNCurrencies")]
        public List<Currency> GetLowestNCurrencies(int Num)
        {
            return unitOfWork.ExchangeHistories.GetLowestNCurrencies(Num);

        }

        [HttpGet("GetMostNImprovedCurrenciesByDate")]
        public List<Currency> GetMostNImprovedCurrenciesByDate(int Num, DateTime Date)
        {
            return unitOfWork.ExchangeHistories.GetMostNImprovedCurrenciesByDate(Num, Date);
        }

        [HttpGet("GetLeastNImprovedCurrenciesByDate")]
        public List<Currency> GetLeastNImprovedCurrenciesByDate(int Num, DateTime Date)
        {
            return unitOfWork.ExchangeHistories.GetLeastNImprovedCurrenciesByDate(Num, Date);
        }
        [HttpGet]
        public ActionResult<double> ConvertAmount(double Amount,int FromCurrencyId, int ToCurrencyId)
        {
            var FromCurrency = unitOfWork.Currencies.Get(FromCurrencyId);
            var ToCurrency = unitOfWork.Currencies.Get(ToCurrencyId);

            var LastRateOfFromCurrency = unitOfWork.ExchangeHistories.GetTheLastRateOfCurrency(FromCurrency);
            var LastRateOfToCurrency = unitOfWork.ExchangeHistories.GetTheLastRateOfCurrency(ToCurrency);
            if(LastRateOfFromCurrency!=0&& LastRateOfToCurrency != 0)
            {
                var ConvertFromCurrencyToDolar = LastRateOfFromCurrency * Amount;
                var ConvertFromDolarToTOCurrncy = ConvertFromCurrencyToDolar / LastRateOfToCurrency;
                return Ok(ConvertFromDolarToTOCurrncy);
            }
            else
            {
                return BadRequest("Cant Be Converted");
            }
            
        }
    }
}

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
    public class CurrencyController : ControllerBase
    {
        UnitOfWork UnitOfWork;
        public CurrencyController()
        {
            UnitOfWork = new UnitOfWork(new AppDbContext());
        }


        [HttpPost("AddCurrency"), Authorize]
        public ActionResult AddCurrency(Currency currency)
        {
            UnitOfWork.Currencies.AddCurrency(currency);
            UnitOfWork.Complete();
            return Ok(UnitOfWork.Currencies.GetCurrencyByName(currency.Name));
        }

        [HttpPut("UpdateCurrency"), Authorize]
        public  ActionResult UpdateCurrency(int Id,Currency NewCurrency)
        {

            var OldCurrency = UnitOfWork.Currencies
                .FindBy(e => e.Id == Id).FirstOrDefault();
            if (OldCurrency != null)
            {
                
                NewCurrency.Id = OldCurrency.Id;
                UnitOfWork.Currencies.Update(NewCurrency);
                UnitOfWork.Complete();
                return Ok(UnitOfWork.Currencies.Get(NewCurrency.Id));
            }
            else
            {
                return BadRequest("No Currency Found");
            }
            
        }

        [HttpPut("DeleteCurrency"), Authorize]
        public ActionResult DeleteCurrency(int Id,Currency currency)
        {
            var OldCurrency = UnitOfWork.Currencies
                .FindBy(e => e.Id == Id).FirstOrDefault();
            if (OldCurrency != null)
            {
                OldCurrency.IsActive = false;
                UnitOfWork.Currencies.Update(OldCurrency);
                UnitOfWork.Complete();
                return Ok(UnitOfWork.Currencies.Get(OldCurrency.Id));
            }
            else
            {
                return BadRequest("Currency Not Found");
            }
        }

        [HttpGet("GetCurrencyByName"), Authorize]
        public ActionResult<Currency> GetCurrencyByName(string Name)
        {
            var currency= UnitOfWork.Currencies.GetCurrencyByName(Name);
            if (currency == null)
            {
                return BadRequest("Currency Not Found");
            }
            else
            {
                return Ok(currency);
            }
        }

        [HttpGet("GetAllCurrencies"), Authorize]
        public ActionResult< IEnumerable<Currency>> GetAllCurrencies()
        {
            var AllCurrencies= UnitOfWork.Currencies.GetAllCurrencies();
            if (AllCurrencies != null)
            {
                return Ok(AllCurrencies);
            }
            else
            {
                return BadRequest("NO Currencies Found");
            }
        }

    }
}

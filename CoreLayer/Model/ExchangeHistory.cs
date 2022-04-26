using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Model
{
    public class ExchangeHistory :BaseEntity
    {
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public DateTime ExchangeDate { get; set; }
        [Required]
        public double Rate { get; set; }
    }
}

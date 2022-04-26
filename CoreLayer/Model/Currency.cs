using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Model
{
    public class Currency : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Sign { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}

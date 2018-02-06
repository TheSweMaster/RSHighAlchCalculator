using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSHighAlchCalculator.Models
{
    public class HighAlchViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long HighAlch { get; set; }
        public long BuyPrice { get; set; }
        public long SellPrice { get; set; }
        public long Profit { get; set; }
        public long BuyQuantity { get; set; }
        public long SellQuantity { get; set; }

    }
}

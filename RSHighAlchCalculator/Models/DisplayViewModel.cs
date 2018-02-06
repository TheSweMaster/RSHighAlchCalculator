using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSHighAlchCalculator.Models
{
    public class DisplayViewModel
    {
        public long NaturePrice { get; set; }
        public long ItemCount { get; set; }
        public long ProfitItemCount { get; set; }
        public List<HighAlchViewModel> HighAlchList { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWallet.Models
{
    public class Podatek
    {
        public int id { get; set; }
        public string ime_stroska { get; set; }
        public decimal cena { get; set; }
        public DateTime datum { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWallet.Models
{
    public class StrategijeVarcevanje
    {
        public int id { get; set; }
        public string opis { get; set; }
        public decimal procent_varcevanja { get; set; }
    }
}

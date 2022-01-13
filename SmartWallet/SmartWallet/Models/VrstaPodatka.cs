using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWallet
{
    public enum Vrsta
    {
        Strosek,
        Prihodek
    }

    public class VrstaPodatka
    {
        public int id { get; set; }
        public Vrsta vrsta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsLibrary
{
    public static class Settings
    {
        //Is the ace high or not
        public static bool AceHigh { get; set; }

        //The order values of all the suits
        public static int ClubsOrder { get; set; }
        public static int DiamondsOrder { get; set; }
        public static int SpadesOrder { get; set; }
        public static int HeartsOrder { get; set; }
        public static int NullOrder { get; set; }
    }
}

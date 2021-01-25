using System;
using System.Collections.Generic;

namespace CalendarioTM.Pages
{
    public static class Extensions
    {
        internal static readonly DateTime _abnormalBloom = new DateTime(2014, 1, 1);

        public static readonly Dictionary<int, string> GregorianMonths = new Dictionary<int, string>()
        {
            [1] = "Gennaio",
            [2] = "Febbraio",
            [3] = "Marzo",
            [4] = "Aprile",
            [5] = "Maggio",
            [6] = "Giugno",
            [7] = "Luglio",
            [8] = "Agosto",
            [9] = "Settembre",
            [10] = "Ottobre",
            [11] = "Novembre",
            [12] = "Dicembre"
        };

        public static readonly Dictionary<int, string> ImperialMonths = new Dictionary<int, string>()
        {
            [1] = "Postapritore",
            [2] = "Forense",
            [3] = "Macinale",
            [4] = "Adulain",
            [5] = "Madrigale",
            [6] = "Granaio",
            [7] = "Lithe",
            [8] = "Antedain",
            [9] = "Solfeggiante",
            [10] = "Orifoglia",
            [11] = "Nembonume",
            [12] = "Dodecabrullo"
        };

        public static readonly Dictionary<int, string> QwaylarMonths = new Dictionary<int, string>()
        {
            [1] = "Xochipilli",
            [2] = "Tonatiuh",
            [3] = "Oxossi",
            [4] = "Snu Snu",
            [5] = "Mami Wata",
            [6] = "Gu",
            [7] = "Shoixal/Lhuixal",
            [8] = "Figli di Mawu/Naa'har",
            [9] = "Mami Tata",
            [10] = "Xangò",
            [11] = "Hebieso",
            [12] = "Sakapta"
        };

        public static readonly Dictionary<int, string> DwarvenMonths = new Dictionary<int, string>()
        {
            [1] = "Cot",
            [2] = "Zert",
            [3] = "Drenat",
            [4] = "Formeghet",
            [5] = "Puste",
            [6] = "Hug",
            [7] = "Wqut",
            [8] = "Ytor",
            [9] = "Jnust",
            [10] = "Morgat",
            [11] = "Archon",
            [12] = "\"Gridopossente\""
        };

        public static readonly Dictionary<int, string> QuenyaMonths = new Dictionary<int, string>()
        {
            [1] = "Narvinyë",
            [2] = "Nénimë",
            [3] = "Súlimë",
            [4] = "Víressë",
            [5] = "Lótessë",
            [6] = "Nárië",
            [7] = "Cermië",
            [8] = "Úrimë",
            [9] = "Yavannië",
            [10] = "Narquelië",
            [11] = "Hísimë",
            [12] = "Ringarë",
            [13] = "Arda"
        };

        public static readonly Dictionary<int, string> SindarMonths = new Dictionary<int, string>()
        {
            [1] = "Narwain",
            [2] = "Nínui",
            [3] = "Gwaeron",
            [4] = "Gwirith",
            [5] = "Lothron",
            [6] = "Nórui",
            [7] = "Cerveth",
            [8] = "Úrui",
            [9] = "Ivanneth",
            [10] = "Narbeleth",
            [11] = "Hithui",
            [12] = "Girthron",
            [13] = "Arda"
        };

        public static (int Year, int Month, int Day) ToImperialCount(this DateTime gregorian)
        {
            return (gregorian.Year - 1736, gregorian.Month, gregorian.Day);
        }

        public static (int Bloom, int Part, int Month, int Day) ToElvenDate(this DateTime gregorian)
        {
            int month = gregorian.DayOfYear / 30 + 1;
            int day = gregorian.DayOfYear % 30;
            if (day == 0)
            {
                day = 30;
                month--;
            }

            double value = (gregorian.Year - 337) / 78;
            int bloom = (int)Math.Round(value, MidpointRounding.AwayFromZero);
            if (gregorian >= _abnormalBloom)
            {
                bloom++;
                int part = (gregorian.Year - 2014) % 78 + 1;
                return (bloom, part, month, day);
            }
            else
            {
                int part = (1737 + gregorian.Year - 337) % 78 + 1;
                return (bloom, part, month, day);
            }
        }
    }
}

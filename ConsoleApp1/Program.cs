using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BR_Gokart_idopontfoglalo__Egyeni_kisprojekt_2025_09_22
{
    internal class Program
    {
        /*
            BR
            Gokart időpontfoglaló - Egyéni kisprojekt
            2025.09.22.
        */
        static void fejlec()
        {
            Type type = typeof(Program);
            string namespaceName = type.Namespace.Replace("_", "-");
            Console.WriteLine(namespaceName);
            for (int i = 0; i < namespaceName.Length; i++) Console.Write('-');
            Console.WriteLine();
        }


        public class Palya
        {
            public string nev = "Rakéta Ring - Gokart Pálya";
            public string cim = "2051, Biatorbágy, Tormásrét utca 4";
            public string tel = "+36 30 229 8845";
            public string web = "raketaring.hu";
        }

        public class Ember
        {
            public string vnev { get; set; }
            public string knev { get; set; }
            public DateTime szul { get; set; }
            public bool elmult => (DateTime.Now.Date - szul.Date).TotalDays >= 18 * 365;
            public string azon
            {
                get
                {
                    var nev = Ekezettavolito3000($"{vnev}{knev}");
                    return $"GO-{nev}-{szul:yyyyMMdd}";
                }
            }
            public string email => $"{Ekezettavolito3000(vnev).ToLower()}.{Ekezettavolito3000(knev).ToLower()}@gmail.com";


            private static string Ekezettavolito3000(string text)
            {
                string ekezettelenit = text.Normalize(NormalizationForm.FormD); // Ödön = O+˙˙+d+o+˙˙+n
                StringBuilder sb = new StringBuilder();
                foreach (var ch in ekezettelenit)
                {
                    var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                    if (uc != UnicodeCategory.NonSpacingMark)
                        sb.Append(ch);
                }
                return sb.ToString().Normalize(NormalizationForm.FormC);// = Odon
            }
        }

        static void Main(string[] args)
        {
            fejlec();
            Palya palya = new Palya();
            Console.WriteLine($"{palya.nev}\n{palya.cim}\n{palya.tel}\n{palya.web}");


            StreamReader be = new StreamReader("vezeteknevek.txt");
            string sor = be.ReadLine();
            string[] nevek = sor.Split(',');
            for (int i = 0; i < nevek.Length; i++)
            {
                nevek[i] = nevek[i].Replace(" ", "").Replace("'", "");
            }

            StreamReader be2 = new StreamReader("keresztnevek.txt");
            string sor2 = be2.ReadLine();
            string[] nevek2 = sor2.Split(',');
            for (int i = 0; i < nevek2.Length; i++)
            {
                nevek2[i] = nevek2[i].Replace(" ", "").Replace("'", "");
            }


            List<Ember> versenyzok = new List<Ember>();

            var rnd = new Random();

            int db = rnd.Next(1, 151);
            for (int i = 0; i < db; i++)
            {
                var vnev = nevek[rnd.Next(nevek.Length)];
                var knev = nevek2[rnd.Next(nevek2.Length)];
                int ev = rnd.Next(1950, 2011);
                int ho = rnd.Next(1, 13);
                int nap = rnd.Next(1, DateTime.DaysInMonth(ev, ho) + 1);
                var szul = new DateTime(ev, ho, nap);
                versenyzok.Add(new Ember
                {
                    vnev = vnev,
                    knev = knev,
                    szul = szul
                });
            }
            foreach (var i in versenyzok)
            {
                Console.WriteLine($"{i.vnev}, {i.knev}, {i.szul:yyyy. MM. dd.}, {i.elmult}, {i.azon}, {i.email}");
            }

            Console.ReadLine();
        }
    }
}

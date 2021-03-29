using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA210329
{
    class Program
    {
        static List<Merkozes> merkozesek = new List<Merkozes>();
        static void Main()
        {
            F02();
            //F03();
            //F04();
            //F05();
            //F06();
            //F07();

            //F03B();
            //F04B();
            //F05B();
            //F06B();
            //F07B();

            F0107C();
            Console.ReadKey();
        }

        private static void F0107C()
        {
            int realHazaiCount = 0;
            int realIdegenCount = 0;
            bool voltDontetlen = false;
            string barcaNev = null;
            var datumMerkozesek = new List<Merkozes>();
            var stadionok = new Dictionary<string, int>();

            foreach (var m in merkozesek)
            {
                if (m.Hazai == "Real Madrid") realHazaiCount++;
                if (m.Idegen == "Real Madrid") realIdegenCount++;
                if (m.HazaiPont == m.IdegenPont) voltDontetlen = true;
                if (m.Hazai.Contains("Barcelona")) barcaNev = m.Hazai;
                if (m.Idopont == "2004-11-21") datumMerkozesek.Add(m); 
                if (!stadionok.ContainsKey(m.Stadion))
                    stadionok.Add(m.Stadion, 1);
                else stadionok[m.Stadion]++;
            }


            Console.WriteLine($"f1: h: {realHazaiCount}, i: {realIdegenCount}");
            Console.WriteLine("f2: " + (voltDontetlen ? "igen" : "nem"));
            Console.WriteLine($"f3: {barcaNev}");
            Console.WriteLine($"f6:");
            foreach (var m in datumMerkozesek)
                Console.WriteLine($"\t{m.Hazai} {m.Idegen} {m.HazaiPont} {m.IdegenPont}");
            Console.WriteLine("f7:");
            foreach (var p in stadionok)
                if (p.Value > 20) Console.WriteLine($"\t{p.Key} {p.Value}");
        }

        private static void F07B()
        {
            Console.WriteLine("7. feladat:");
            var dic = new Dictionary<string, int>();
            foreach (var m in merkozesek)
            {
                if(!dic.ContainsKey(m.Stadion))
                    dic.Add(m.Stadion, 1);
                else dic[m.Stadion]++;
            }
            foreach (var kvp in dic)
            {
                if(kvp.Value > 20)
                {
                    Console.WriteLine($"\t{kvp.Key}: {kvp.Value}");
                }
            }
        }

        private static void F06B()
        {
            Console.WriteLine("6. feladat:");

            foreach (var m in merkozesek)
            {
                if(m.Idopont == "2004-11-21") 
                    Console.WriteLine($"\t{m.Hazai} - {m.Idegen} ({m.HazaiPont}:{m.IdegenPont})");
            }
        }

        private static void F05B()
        {
            Console.Write("5. feladat: barcelonai csapat neve: ");
            int i = 0;
            while (!merkozesek[i].Hazai.Contains("Barcelona"))
            {
                i++;
            }
            Console.WriteLine(merkozesek[i].Hazai);
        }

        private static void F04B()
        {
            Console.Write("4. feladat: volt döntetlen? ");
            int i = 0;
            while (i < merkozesek.Count && merkozesek[i].HazaiPont != merkozesek[i].IdegenPont)
            {
                i++;
            }
            Console.WriteLine(i < merkozesek.Count ? "igen" : "nem");
        }

        private static void F03B()
        {
            Console.Write("3. feladat: Real Madrid: ");
            int hc = 0;
            int ic = 0;
            foreach (var m in merkozesek)
            {
                if (m.Hazai == "Real Madrid") hc++;
                if (m.Idegen == "Real Madrid") ic++;
            }
            Console.WriteLine($"Hazai: {hc}, Idegen: {ic}");
        }

        private static void F07()
        {
            Console.WriteLine("7. feladat:");
            merkozesek.GroupBy(x => x.Stadion).Where(y => y.Count() > 20)
                .ToList().ForEach(a => Console.WriteLine($"\t{a.Key}: {a.Count()}"));
        }
        private static void F06()
        {
            Console.WriteLine("6. feladat:");
            merkozesek.Where(x => x.Idopont == "2004-11-21").ToList()
                .ForEach(a => Console.WriteLine($"\t{a.Hazai} - {a.Idegen} ({a.HazaiPont}:{a.IdegenPont})"));
        }
        private static void F05()
        {
            Console.WriteLine("5. feladat: barcelonai csapat neve: " +
                $"{merkozesek.Where(x=> x.Hazai.Contains("Barcelona")).First().Hazai}");
        }
        private static void F04()
        {
            Console.WriteLine("4. feladat: Volt döntetlen? " +
                $"{(merkozesek.Any(x => x.HazaiPont == x.IdegenPont) ? "igen" : "nem")}");
        }
        private static void F03()
        {
            Console.WriteLine("3. feladat: Real Madrid: " +
                $"Hazai: {merkozesek.Count(x => x.Hazai == "Real Madrid")}, " +
                $"Idegen: {merkozesek.Count(x => x.Idegen == "Real Madrid")}");
        }                
        private static void F02()
        {
            var sr = new StreamReader("eredmenyek.csv", Encoding.UTF8);
            sr.ReadLine();
            while (!sr.EndOfStream)
                merkozesek.Add(new Merkozes(sr.ReadLine()));
            sr.Close();
        }
    }
}

using pro;
using System;
using System.Collections.Generic;
using System.IO;
using static Szemely;


class Szemely
{
    private string nev;
    private string lak { get; set; }
    public int[] szuletesiDatum { get; set; }

    public Szemely(string nev)
    {
        this.nev = nev;
        this.lak = "";
        this.szuletesiDatum = new int[3];
    }

    public Szemely(string nev, string lakcim)
    {
        this.nev = nev;
        this.lak = lakcim;
        this.szuletesiDatum = new int[3];
    }

    public Szemely(string nev, int[] szuletesiDatum)
    {
        this.nev = nev;
        this.lak = "";
        this.szuletesiDatum = szuletesiDatum;
    }

    public Szemely(string nev, string lakcim, int[] szuletesiDatum)
    {
        this.nev = nev;
        this.lak = lakcim;
        this.szuletesiDatum = szuletesiDatum;
    }

    public string Nev
    {
        get { return nev; }
    }

    public string Lakcim
    {
        get { return lak; }
        set { lak = value; }
    }

    public int this[int index]
    {
        get
        {
            if (index >= 0 && index < 3)
                return szuletesiDatum[index];
            else
                throw new IndexOutOfRangeException("Érvénytelen index.");
        }
    }

    public override string ToString()
    {
        return $"Név: {nev}, Lakcím: {lak}, Születési dátum: {szuletesiDatum[0]}/{szuletesiDatum[1]}/{szuletesiDatum[2]}";
    }

    public int Kor()
    {
        int ev = DateTime.Now.Year;
        return ev - szuletesiDatum[0];
    }

    public class Vallalat
    {
        public Dictionary<int, List<Alkalmazott>> OsztalyonkentiAlkalmazottak { get; } = new Dictionary<int, List<Alkalmazott>>();
        private static string[] beosztasok = { "Nyugdíjas", "Ács", "Faszállító", "Takarító" };
        public void BelepAlkalmazott(int osztalyKod, Alkalmazott alkalmazott)
        {
            if (!OsztalyonkentiAlkalmazottak.ContainsKey(osztalyKod))
            {
                OsztalyonkentiAlkalmazottak[osztalyKod] = new List<Alkalmazott>();
            }

            OsztalyonkentiAlkalmazottak[osztalyKod].Add(alkalmazott);
        }

        public void KilepAlkalmazott(int osztalyKod, Alkalmazott alkalmazott)
        {
            if (OsztalyonkentiAlkalmazottak.ContainsKey(osztalyKod))
            {
                OsztalyonkentiAlkalmazottak[osztalyKod].Remove(alkalmazott);
            }
        }

        public void ModositAlkalmazott(int osztalyKod, Alkalmazott regiAlkalmazott, Alkalmazott ujAlkalmazott)
        {
            if (OsztalyonkentiAlkalmazottak.ContainsKey(osztalyKod))
            {
                int index = OsztalyonkentiAlkalmazottak[osztalyKod].IndexOf(regiAlkalmazott);
                if (index >= 0)
                {
                    OsztalyonkentiAlkalmazottak[osztalyKod][index] = ujAlkalmazott;
                }
            }
        }

        public void NyugdijbaMegy(int osztalyKod, Alkalmazott alkalmazott)
        {
            KilepAlkalmazott(osztalyKod, alkalmazott);
        }

        public void MentAlkalmazottak(string fajlnev)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(fajlnev))
                {
                    foreach (var osztalyKod in OsztalyonkentiAlkalmazottak.Keys)
                    {
                        foreach (var alkalmazott in OsztalyonkentiAlkalmazottak[osztalyKod])
                        {
                            // Példa CSV formátumra: Név,Beosztás,Fizetés
                            string sor = $"{alkalmazott.Nev},{Vallalat.beosztasok[alkalmazott.Beosztas]},{alkalmazott.fizetes}";
                            file.WriteLine(sor);
                        }
                    }
                }

                Console.WriteLine("Az alkalmazottak sikeresen el lettek mentve a fájlba.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Hiba a mentés során: {e.Message}");
            }
        }

    }


}



class Alkalmazott : Szemely
{
    private static string[] beosztasok = { "Nyugdíjas", "Ács", "Faszállító", "Takarító" };

    private int beosztas; // Beosztás kódja
    public int fizetes { get; set; }
    public int OsztalyKod { get; set; }

    public Alkalmazott(string nev, int osztalyKod) : base(nev)
    {
        this.beosztas = osztalyKod; // Beosztás beállítása
        this.OsztalyKod = osztalyKod;
    }

    public Alkalmazott(string nev, int beosztas, int fizetes) : base(nev)
    {
        this.beosztas = beosztas;
        this.fizetes = fizetes;
    }

    public int Beosztas
    {
        get { return beosztas; }
        set { beosztas = value; } // Beosztás kódjának beállítása
    }

    public override string ToString()
        {
            return $"{Nev}, Beosztás: {beosztasok[beosztas]}, Fizetés: {fizetes} Ft"; // Beosztás frissítése
        }

    public void FizetesEmeles(int emeles)
    {
        if (fizetes > 299999)
        {
            fizetes += emeles;
        }
    }

    public new int Kor()
    {
        if (fizetes > 299999)

            return base.Kor() - 10;
        else
            return base.Kor();
    }

}

class Program
{
    static void Main()
    {
        Vallalat vallalat = new Vallalat();

        Console.WriteLine("Személy létrehozása:");
        Console.Write("Név: ");
        string nev = Console.ReadLine();
        Console.Write("Lakcím: ");
        string lakcim = Console.ReadLine();
        Console.Write("Születési év: ");
        int ev = int.Parse(Console.ReadLine());
        Console.Write("Születési hónap: ");
        int honap = int.Parse(Console.ReadLine());
        while(honap > 12)
        {
            Console.Write("Születési hónap: ");
            honap = int.Parse(Console.ReadLine());
        }
        Console.Write("Születési nap: ");
        int nap = int.Parse(Console.ReadLine());
        while(nap > 31)
        {
            Console.Write("Születési nap: ");
            nap = int.Parse(Console.ReadLine());
        }

        int[] szuletesiDatum = { ev, honap, nap };

        Szemely szemely = new Szemely(nev, lakcim, szuletesiDatum);

        Console.WriteLine("Alkalmazott létrehozása:");
        Console.Write("Név: ");
        nev = Console.ReadLine();
        Console.Write("Fizetés: ");
        int fizetes = Convert.ToInt32(Console.ReadLine());
        int osztalyKod = 0;
        Alkalmazott alkalmazott = new Alkalmazott(nev, osztalyKod, fizetes);
        Console.WriteLine(alkalmazott.ToString());
        while (osztalyKod == 0)
        {
            Console.WriteLine("1. Nyugdíjas\n2. Dolgozik");
            int nyugdije = Convert.ToInt32(Console.ReadLine());
            if (nyugdije == 1)
            {
                osztalyKod++;
                vallalat.NyugdijbaMegy(osztalyKod, alkalmazott);
                break;
            }
            Console.WriteLine("Beosztások:\n1. Ács\n2. Faszállító\n3. Takarító");
            Console.Write("Válassz az alábbi beosztások közül: ");
            osztalyKod = Convert.ToInt32(Console.ReadLine());
            vallalat.BelepAlkalmazott(osztalyKod, alkalmazott);

            if (osztalyKod != 0)
            {
                Console.WriteLine("Biztos ezt a munkát akarja?");
                Console.WriteLine("Igen\nNem");
                string marad = Convert.ToString(Console.ReadLine());
                if (marad == "Nem")
                {
                    osztalyKod = 0;
                    vallalat.KilepAlkalmazott(osztalyKod, alkalmazott);
                }
            }
        }

        alkalmazott.Beosztas = osztalyKod;
        Console.WriteLine("\nSzemély adatok:");
        Console.WriteLine(szemely);
        Console.WriteLine($"Életkor: {szemely.Kor()} év");



        Console.WriteLine("\nAlkalmazott adatok:");
        Console.WriteLine(alkalmazott);
        
        Console.WriteLine("\nFizetésemelés (300000 Ft felett):");
        Console.WriteLine($"Új fizetés: {alkalmazott.fizetes} Ft");
        Console.WriteLine($"Életkor: {alkalmazott.Kor() - szemely.szuletesiDatum[0]} év");


        // Típuskényszerítés példa
        Szemely szemely2 = alkalmazott;
        Console.WriteLine("\nTípuskényszerítés:");
        Console.WriteLine(szemely2);
        vallalat.MentAlkalmazottak("alkalmazottak.csv");
        Console.ReadLine();

    }
}
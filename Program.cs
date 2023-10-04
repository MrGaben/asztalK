using System;

class Szemely
{
    private string nev;
    private string lak { get; set; }
    private int[] szuletesiDatum { get; set; }

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
}

class Alkalmazott : Szemely
{
    private string beosztas;
    private double fizetes;

    public Alkalmazott(string nev) : base(nev)
    {
        this.beosztas = "";
        this.fizetes = 0;
    }

    public Alkalmazott(string nev, string beosztas, double fizetes) : base(nev)
    {
        this.beosztas = beosztas;
        this.fizetes = fizetes;
    }

    public double Fizetes
    {
        get { return fizetes; }
        set { fizetes = value; }
    }

    public string Beosztas
    {
        get { return beosztas; }
    }

    public override string ToString()
    {
        return base.ToString() + $", Beosztás: {beosztas}, Fizetés: {fizetes} Ft";
    }

    public void FizetesEmeles(double emeles)
    {
        fizetes += emeles;
    }

    public new int Kor()
    {
        if (fizetes > 300000)
            return base.Kor() - 10;
        else
            return base.Kor();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Személy létrehozása:");
        Console.Write("Név: ");
        string nev = Console.ReadLine();
        Console.Write("Lakcím: ");
        string lakcim = Console.ReadLine();
        Console.Write("Születési év: ");
        int ev = int.Parse(Console.ReadLine());
        Console.Write("Születési hónap: ");
        int honap = int.Parse(Console.ReadLine());
        Console.Write("Születési nap: ");
        int nap = int.Parse(Console.ReadLine());

        int[] szuletesiDatum = { ev, honap, nap };

        Szemely szemely = new Szemely(nev, lakcim, szuletesiDatum);

        Console.WriteLine("\nAlkalmazott létrehozása:");
        Console.Write("Név: ");
        nev = Console.ReadLine();
        Console.Write("Beosztás: ");
        string beosztas = Console.ReadLine();
        Console.Write("Fizetés: ");
        double fizetes = double.Parse(Console.ReadLine());

        Alkalmazott alkalmazott = new Alkalmazott(nev, beosztas, fizetes);

        Console.WriteLine("\nSzemély adatok:");
        Console.WriteLine(szemely);
        Console.WriteLine($"Életkor: {szemely.Kor()} év");

        Console.WriteLine("\nAlkalmazott adatok:");
        Console.WriteLine(alkalmazott);
        Console.WriteLine($"Életkor: {szemely.Kor()} év");

        Console.WriteLine("\nFizetésemelés (300000 Ft felett):");
        alkalmazott.FizetesEmeles(10000);
        Console.WriteLine($"Új fizetés: {alkalmazott.Fizetes} Ft");
        Console.WriteLine($"Új életkor: {szemely.Kor()} év");

        // Típuskényszerítés példa
        Szemely szemely2 = alkalmazott;
        Console.WriteLine("\nTípuskényszerítés:");
        Console.WriteLine(szemely2);

        Console.ReadLine();
    }
}
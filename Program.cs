//proizvod: ime, količina, cijena i datum isteka (isti za sve proizvode istog tipa).
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Text;

var Artikli = new List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)>()
{
    ( "mlijeko", 25, 2.00f, new DateTime(2023,11,20)),
    ("keksi", 10, 1.50f, new DateTime(2024, 1, 6)),
    ( "sladoled", 30, 1.00f, new DateTime(2023,11,29)),
    ("pahuljice", 20, 1.80f, new DateTime(2024, 1, 19)),
    ("sok od jabuke", 12, 1.40f, new DateTime(2022, 11, 17)),
    ("piletina", 15, 2.25f, new DateTime(2023, 11, 30)),
    ("jaja", 17, 2f, new DateTime(2023, 11, 11))
};
var Radnici = new List<(string imePrezime, DateTime rodenje)>()
{
    ("mate matic", new DateTime(2000, 11, 3)),
    ("sara saric", new DateTime(1980, 4, 6)),
    ("karla karlic", new DateTime(1955, 11, 23))
};
var Racuni = new Dictionary<int, (DateTime vrijemeIzdavanja, List<(string naziv, int kolicina)> proizvodi)>()
{
    { 1,(new DateTime(2023, 11, 17), new List<(string naziv, int kolicina)>(){("mlijeko", 3),("keksi", 1)})},
    { 2,(new DateTime(2023, 10,1), new List<(string naziv, int kolicina)>(){ ("piletina", 1),("keksi", 2), ("mlijeko", 4)})},
    { 3,(new DateTime(2023, 10,20), new List<(string naziv, int kolicina)>(){("sladoled", 3)})}
};
static int Godine(DateTime datum)
{
    var godine = DateTime.Now.Year - datum.Year;
    if (DateTime.Now.DayOfYear < datum.DayOfYear)
    {
        godine--;
    }
    return godine;
}

static int UnosRasponaIntegera(int donjaGranica, int gornjaGranica)
{
    var odabir = 0;
    int.TryParse(Console.ReadLine(), out odabir);
    var unosOdabira = true;
    while (unosOdabira)
    {
        if (odabir >= donjaGranica && odabir < gornjaGranica)
        {
            unosOdabira = false;
        }
        else
        {
            Console.WriteLine();
            Console.Write("Unesi ponovno:");
            int.TryParse(Console.ReadLine(), out odabir);
        }
    }
    return odabir;
}
static float ProvjeraUnosaFloat(float broj)
{
    float noviBroj = broj;
    float.TryParse(Console.ReadLine(), out noviBroj);
    var unosCijene = true;

    while (unosCijene)
    {
        if (noviBroj<= 0)
        {
            Console.Write("Nemožete unjeti taj broj. \nPonovite unos: ");
            float.TryParse(Console.ReadLine(), out noviBroj);
        }
        else
        {
            unosCijene = false;
        }
    }
    return noviBroj;
}

static int ProvjeraUnosaInt(int broj)
{
    int noviBroj = broj;
    int.TryParse(Console.ReadLine(), out noviBroj);
    var unosCijene = true;

    while (unosCijene)
    {
        if (noviBroj <= 0)
        {
            Console.Write("Nemožete unjeti taj broj. \nPonovite unos: ");
            int.TryParse(Console.ReadLine(), out noviBroj);
        }
        else
        {
            unosCijene = false;
        }
    }
    return noviBroj;
}
static int DoIsteka(DateTime navedenoVrijeme)
{
    var trenutnoVrijme = DateTime.Now;
    var doIsteka = 0;
    if (trenutnoVrijme.Year == navedenoVrijeme.Year)
    {
        doIsteka = navedenoVrijeme.DayOfYear - trenutnoVrijme.DayOfYear;

    }
    else
    {
        var razlikaIzmeduGodina = navedenoVrijeme.Year - trenutnoVrijme.Year;
        var godinaIzmedu = (razlikaIzmeduGodina - 1) * 365;

        var danaDokrajaGodine = 365 - trenutnoVrijme.DayOfYear;
        doIsteka = danaDokrajaGodine + godinaIzmedu + navedenoVrijeme.DayOfYear;
    }
    return doIsteka;
}
static bool Potvrda()
{
    var unos = true;
    var unosPonavljanjaOdabir = Console.ReadLine().ToLower();
    var unosPonavnjanja = true;
    while (unosPonavnjanja)
    {
        switch (unosPonavljanjaOdabir)
        {
            case "da":
                unosPonavnjanja = false;
                break;
            case "ne":
                unos = false;
                unosPonavnjanja = false;
                break;
            default:
                Console.Write("Neispravno upisano. Ponovite unos: ");
                unosPonavljanjaOdabir = Console.ReadLine().ToLower();
                break;
        }
    }
    return unos;
}

bool pocetniEkran = true;
while (pocetniEkran)
{
    Console.Clear();
    Console.WriteLine("...\n");
    Console.WriteLine("1 - Artikli \n2 - Radnici \n3 - Racuni \n4 - Statistika \n0 - Izlaz iz aplikacije ");
    Console.Write("\nUnesi broj: ");
    int pocetniOdabir = UnosRasponaIntegera(0, 5);

    switch (pocetniOdabir)
    {
        case 1:
            var izlazArtikli = false;
            while (!izlazArtikli)
            {
                Console.Clear();
                Console.WriteLine("Artikli > ...\n");
                Console.WriteLine("1 - Unos Artikla \n2 - Brisanje artikla \n3 - Uredivanje Artikla \n4 - Ispis Artikla \n0 - Natrag");
                Console.Write("\nUnesi broj: ");
                var artikliOdabir = UnosRasponaIntegera(0, 5);
                switch (artikliOdabir)
                {
                    case 0:
                        izlazArtikli = true;
                        break;
                    case 1:
                        Artikli = UnosArtikla(Artikli);
                        break;
                    case 2:
                        var izlazBrisanjeArtikla = false;
                        while (!izlazBrisanjeArtikla)
                        {
                            Console.Clear();
                            Console.WriteLine("Artikli > Brisanje Artikla > ...\n");
                            Console.WriteLine("a - Brisanje po imenu artika\nb - Brisanje svih artikla kojima je istekao datum trajanja\n0 - natrag");
                            Console.Write("\nUnesi slovo: ");
                            string unosBrisanjaArtikla = Console.ReadLine();
                            var ponoviUnos = true;
                            while (ponoviUnos)
                            {
                                switch (unosBrisanjaArtikla)
                                {
                                    case "0":
                                        ponoviUnos = false;
                                        izlazBrisanjeArtikla = true;
                                        break;
                                    case "a":
                                        ponoviUnos = false;
                                        BrisanjeArtikla(Artikli);
                                        break;
                                    case "b":
                                        BrisanjeArtiklaPoDatumu(Artikli);
                                        ponoviUnos = false;
                                        break;
                                    default:
                                        Console.Write("\nNepostojeći odabir. \nPonovi unos: ");
                                        unosBrisanjaArtikla = Console.ReadLine();
                                        break;
                                }
                            }
                        }
                        break;
                    case 3:
                        var izlazUredivanjeArtikla = false;
                        while (!izlazUredivanjeArtikla)
                        {
                            Console.Clear();
                            Console.WriteLine("Artikli > Uredivanje Artikla > ...\n");
                            Console.WriteLine("a - Uredivanje zasebnog artikla \nb - Popust ili poskupljenje na sve proizvode u trgovini\n0 - natrag");
                            Console.Write("\nUnesi slovo: ");
                            var ponoviUnos = true;
                            while (ponoviUnos)
                            {
                                switch (Console.ReadLine())
                                {
                                    case "a":
                                        Artikli = UredivanjeArtikla(Artikli);
                                        ponoviUnos = false;
                                        break;
                                    case "b":
                                        ponoviUnos = false;
                                        Artikli = UredivanjeArtiklaPopust(Artikli);
                                        break;
                                    case "0":
                                        ponoviUnos = false;
                                        izlazUredivanjeArtikla = true;
                                        break;
                                    default:
                                        Console.Write("\nNepostojeći odabir. \nPonovi unos: ");
                                        break;
                                }
                            }
                        }
                        break;
                    case 4:
                        var izlazIspisArtikla = false;
                        //(format: ime(količina) - cijena - broj dana od /do isteka)
                        while (!izlazIspisArtikla)
                        {
                            Console.Clear();
                            Console.WriteLine("Artikli > Ispis Artikla\n");
                            Console.WriteLine("a - Ispis artikla kako su spremljeni \nb - Ispis artikla po imenu \nc - Ispis artikla po datumu silazno \nd - Ispis artikla po datumu uzlazno\ne - Ispis artikla po količini\nf - ispis najprodavanijeg artikla\ng - ispis namjanje prodavanog artikla \n0 - natrag");
                            Console.Write("\nUnesi slovo: ");
                            string unosIspisaArtikla = Console.ReadLine();
                            var ponoviUnos = true;
                            while (ponoviUnos)
                            {
                                switch (unosIspisaArtikla)
                                {
                                    case "0":
                                        ponoviUnos = false;
                                        izlazIspisArtikla = true;
                                        break;
                                    case "a":
                                        ponoviUnos = false;
                                        IspisArtikla(Artikli);
                                        break;
                                    case "b":
                                        ponoviUnos = false;
                                        IspisArtiklaIme(Artikli);
                                        break;
                                    case "c":
                                        ponoviUnos = false;
                                        IspisArtiklaDatum(Artikli, true);
                                        break;
                                    case "d":
                                        ponoviUnos = false;
                                        IspisArtiklaDatum(Artikli, false);
                                        break;
                                    case "e":
                                        ponoviUnos = false;
                                        IspisArtiklaKolicina(Artikli);
                                        break;
                                    case "f":
                                        ponoviUnos = false;
                                        var najviseProdavanArtikl = NajviseProdavanArtikl(Racuni);
                                        if (najviseProdavanArtikl.Count == 1)
                                        {
                                            Console.WriteLine($"\nNajprodavaniji artikl je {najviseProdavanArtikl[0].naziv}, koji je prodan {najviseProdavanArtikl[0].kolicina} puta");
                                        }
                                        else
                                        {
                                            Console.Write($"\nProizvodi koji su najprodavaniji ({najviseProdavanArtikl[0].kolicina} puta) su: ");
                                            var proizvodiIspis = new StringBuilder("");
                                            for (int i = 0; i < najviseProdavanArtikl.Count; i++)
                                            {
                                                proizvodiIspis.Append(najviseProdavanArtikl[i].naziv);
                                                if (i != najviseProdavanArtikl.Count)
                                                {
                                                    proizvodiIspis.Append(", ");
                                                }
                                            }
                                            Console.WriteLine(proizvodiIspis.ToString());

                                        }
                                        break;
                                    case "g":
                                        ponoviUnos = false;
                                        var najmanjeProdavanArtikl = NajmanjeProdavanArtikl(Racuni, Artikli);
                                        if(najmanjeProdavanArtikl.Count == 1)
                                        {
                                            Console.WriteLine($"\nNajmanje prodavan artikl je {najmanjeProdavanArtikl[0].naziv}, koji je prodan {najmanjeProdavanArtikl[0].kolicina} puta");
                                        }
                                        else
                                        {
                                            Console.Write($"\nProizvodi koji su prodani najmanje puta ({najmanjeProdavanArtikl[0].kolicina} puta) su: ");
                                            var proizvodiIspis = new StringBuilder("");
                                            for(int i = 0; i < najmanjeProdavanArtikl.Count; i++)
                                            {
                                                proizvodiIspis.Append(najmanjeProdavanArtikl[i].naziv);
                                                if(i != najmanjeProdavanArtikl.Count)
                                                {
                                                    proizvodiIspis.Append(", ");
                                                }
                                            }
                                            Console.WriteLine(proizvodiIspis.ToString());

                                        }
                                        break;
                                    default:
                                        Console.Write("\nNepostojeći odabir. \nPonovi unos: ");
                                        unosIspisaArtikla = Console.ReadLine();
                                        break;
                                }
                            }
                            if (unosIspisaArtikla != "0")
                            {
                                Console.WriteLine("\nStisni ENTER za drugi unos.");
                                Console.ReadLine();
                            }
                        } 

                        break;
                }
            }
            break;
        case 2:
            var izlazRadnici= false;
            while (!izlazRadnici)
            {
                Console.Clear();
                Console.WriteLine("Radnici > ...\n");
                Console.WriteLine("1 - Unos radnika \n2 - Brisanje radnika \n3 - Uredivanje radnika \n4 - Ispis radnika \n0 - Natrag");
                Console.Write("\nUnesi broj: ");
                var unos = UnosRasponaIntegera(0, 5);

                switch (unos)
                {
                    case 0:
                        izlazRadnici = true;
                        break;
                    case 1:
                        Radnici = UnosRadnika(Radnici);
                        break;
                    case 2:
                        var izlazBrisanjeRadnici = false;
                        while (!izlazBrisanjeRadnici)
                        {
                            Console.Clear();
                            Console.WriteLine("Radnici > Brisanje Radnika > ...\n");
                            Console.WriteLine("a - Brisanje radnika po imenu\nb - Brsianje radnika starijih od 65\n0 - Natrag");
                            Console.Write("\nUnesi slovo: ");
                            string unosBrisanjaRadnika = Console.ReadLine();
                            var ponoviUnos = true;
                            while (ponoviUnos)
                            {
                                switch (unosBrisanjaRadnika)
                                {
                                    case "a":
                                        BrisanjeRadnika(Radnici);
                                        ponoviUnos = false;
                                        break;
                                    case "b":
                                        ponoviUnos = false;
                                        BrisanjeRadniciGodine(Radnici);
                                        break;
                                    case "0":
                                        ponoviUnos = false;
                                        izlazBrisanjeRadnici = true;
                                        break;

                                    default:
                                        Console.Write("Neispravan unos.\nUnesi ponovno: ");
                                        unosBrisanjaRadnika = Console.ReadLine();
                                        break;
                                }

                            }
                        }
                        break;
                    case 3:
                        Console.Clear();

                        Radnici = UredivanjeRadnika(Radnici);
                        break;
                    case 4:
                        var izlazIspisRadnici = false;
                        while (!izlazIspisRadnici)
                        {
                            Console.Clear();
                            Console.WriteLine("Radnici > Ispis Radnika\n");
                            Console.WriteLine("a - Ispis svih radnika\nb - Ispis ranika kojima je rodendan u tekućem mjesecu\n0 - Natrag");
                            Console.Write("\nUnesi slovo: ");
                            string unosIspisaRadnika = Console.ReadLine();
                            var ponoviUnos = true;
                            while (ponoviUnos)
                            {
                                switch (unosIspisaRadnika)
                                {
                                    case "0":
                                        ponoviUnos = false;
                                        izlazIspisRadnici = true;
                                        break;
                                    case "a":
                                        IspisRadnika(Radnici);
                                        ponoviUnos = false;
                                        break;
                                    case "b":
                                        IspisRadnikaTekuciMjesec(Radnici);
                                        ponoviUnos = false;
                                        break;
                                    default:
                                        Console.Write("Neispravan unos.\nUnesi ponovno: ");
                                        unosIspisaRadnika = Console.ReadLine();
                                        break;
                                }
                            }
                            if (unosIspisaRadnika != "0")
                            {
                                Console.WriteLine("\nStisni ENTER za drugi unos.");
                                Console.ReadLine();
                            }
                        }

                        break;
                }
            }
            break;
        case 3:
            var izlazRacuni = false;
            while (!izlazRacuni)
            {
                Console.Clear();
                Console.WriteLine("Racuni > ...\n");
                Console.WriteLine("1 - Unos Novog računa \n2 - Ispis Računa \n0 - Natrag");
                Console.Write("\nUnesi broj: ");
                var unos = UnosRasponaIntegera(0, 3);

                switch (unos)
                {
                    case 0:
                        izlazRacuni = true;
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }
            break;
        case 4:
            var izlazStatistika = false;
            while (!izlazStatistika)
            {
                Console.Clear();
                Console.WriteLine("Statistika > ...\n");
                var predefiniranaSifra = "sifra123";

                Console.Write("Unesi sifru za nastavak: ");
                var unesenaSifra = Console.ReadLine();
                var tocnostSifre = true;
                if (predefiniranaSifra.Length == unesenaSifra.Length)
                {
                    for (int i = 0; i < predefiniranaSifra.Length; i++)
                    {
                        if (predefiniranaSifra[i] != unesenaSifra[i])
                        {
                            tocnostSifre = false;
                        }
                    }
                }
                else
                {
                    tocnostSifre = false;
                }

                if (tocnostSifre)
                {
                    Console.WriteLine("Sifra je ispravna. \n");
                    Console.WriteLine("1 - Ukupan broj artikala u trgovini \n2 - Vrijednost artikala koji nisu još prodani \n3 - Vrijednost svih artikala koji su prodani \n4 - Stanje po mjesecima \n0 - Natrag");
                    Console.Write("\nUnesi broj: ");
                    var unos = UnosRasponaIntegera(0, 5);

                    switch (unos)
                    {
                        case 0:
                            izlazStatistika = true;
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Sifra je neispravna. Stisnite ENTER za povratak na početni ekran.");
                    Console.ReadKey();
                    izlazStatistika = true;
                }
            }
            break;
        case 0:
            Console.WriteLine("Stisni bilo koji ključ na tipkovnici za izlaz.");
            pocetniEkran = false;
            break;
    }
}

static List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> UnosArtikla(List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikli)
{
    Console.Clear();
    Console.WriteLine("Artikli > Unos Artikla \n");
    var prosliArtikli = new List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)>();
    foreach(var proizvod in artikli)
    {
        prosliArtikli.Add(proizvod);
    }
    var unos = true;
    while (unos) 
    {
        Console.Write("Unesi naziv novog artikla: ");
        var nazivArtikla = Console.ReadLine().ToLower();

        Console.Write("\nUnesi količinu tog artikla: ");
        var kolicinaArtikla = ProvjeraUnosaInt(0);

        Console.Write("\nUnesi cijenu u eurima: ");
        float cijenaArtikla = ProvjeraUnosaFloat(0);
        float decimalaCijene = (float)((int)((cijenaArtikla % 1) * 100)) / 100;
        cijenaArtikla = (int)cijenaArtikla + decimalaCijene;

        Console.Write("\nUnesi godinu isteka roka: ");
        var godina = ProvjeraUnosaInt(0);
        Console.Write("\nUnesi mjesec isteka roka: ");
        var mjesec = UnosRasponaIntegera(0, 13);
        Console.Write("\nUnesi dan isteka roka: ");
        int dan;
        switch (mjesec)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                dan = UnosRasponaIntegera(1, 32);
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                dan = UnosRasponaIntegera(1, 31);
                break;
            default:
                dan = UnosRasponaIntegera(1, 29);
                break;
        }

        artikli.Add((naziv: nazivArtikla, kolicina: kolicinaArtikla, cijena: cijenaArtikla, datumIstekaRoka: new DateTime(godina, mjesec, dan)));

        Console.Write("\nZelite li unijeti jos jedan aritikal? (da / ne) ");
        unos = Potvrda();
    }

    Console.Write("\nPotvrđujete li ove promjene? (da / ne) ");
    var potvda = Potvrda();
    if (potvda)
    {
        return artikli;
    }
    else
    {
        return prosliArtikli;
    }

}
static void BrisanjeArtikla(List < (string naziv, int kolicina, float cijena, DateTime datumIstekaRoka) > artikli)
{
    Console.Clear();
    Console.WriteLine("Artikli > Brisanje Artikla > Po imenu\n");
    var artiklPronaden = false;
    string unos;
    while (!artiklPronaden)
    {
        Console.Write("Unesi naziv artikla kojeg želiš izbrisati: ");
        unos = Console.ReadLine().ToLower();

        for(int i = 0; i < artikli.Count; i++)
        {
            if (artikli[i].naziv == unos && !artiklPronaden)
            {
                Console.Write("Jeste li sigurni da želite izbrisati ovaj artikl? (da / ne) ");
                if (Potvrda())
                {
                    artikli.Remove(artikli[i]);
                    Console.WriteLine("Artikl {0} uspjesno izbrisan. ", unos);
                }

                i = artikli.Count - 1;
                artiklPronaden = true;
                Console.WriteLine("Stisnite ENTER za drugi unos.");
                Console.ReadLine();
            }
        }

        if (!artiklPronaden)
        {
            Console.Write("Nepostoji taj artikl u bazi. \nZelite li pokušati ponovno unijeti? (da / ne) ");
            artiklPronaden = !Potvrda();
        }
    }
}
static void BrisanjeArtiklaPoDatumu(List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikli)
{

    Console.Write("Zelite li potvriti brisanje svih artikla kojima je istekao datum? (da / ne)");
    var potvrda = Potvrda();
    if (potvrda)
    {
        for(int i = 0; i < artikli.Count; i++)
        {
            if (DoIsteka(artikli[i].datumIstekaRoka) < 0)
            {
                artikli.Remove(artikli[i]);
            }
        }
    }
    Console.WriteLine("\nStisni ENTER za drugi unos.");
    Console.ReadLine();
}
static List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> UredivanjeArtikla(List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikli)
{
    Console.Clear();
    Console.WriteLine("Artikli > Uređivanje Artikla > Zasebno Proizvoda");
    var artiklPronaden = false;
    string unos;
    while (!artiklPronaden)
    {
        Console.Write("\nUnesi naziv artikla kojeg želiš urediti: ");
        unos = Console.ReadLine().ToLower();
        var urediDrugiArtikl = false;
        for (int i = 0; i < artikli.Count; i++)
        {
            if (artikli[i].naziv == unos && !artiklPronaden)
            {
                artiklPronaden = true;
                var proizvod = artikli[i];
                i = artikli.Count - 1;
                Console.WriteLine("\nKoje svojstvo proizvoda {0} želite promijeniti? ", unos);
                Console.WriteLine("1 - Naziv\n2 - kolicina\n3 - cijena\n4 - datum isteka roka\n0 -ponisti uredivanje i izaberi drugi artikl za urediti");
                Console.Write("Unesi broj: ");
                var pronadenoSvojstvo = false;
                while (!pronadenoSvojstvo)
                {
                    switch (Console.ReadLine())
                    {
                        case "0":
                            Console.WriteLine("Vise ne uredujete proizvod {0}.", unos);
                            artiklPronaden = false;
                            urediDrugiArtikl = true;
                            pronadenoSvojstvo = true;
                            break;
                        case "1":
                            pronadenoSvojstvo = true;
                            Console.Write("Unesi novi naziv za proizvod {0}: ", proizvod.naziv);
                            var noviProizvod = (Console.ReadLine().ToLower(), proizvod.kolicina, proizvod.cijena, proizvod.datumIstekaRoka);
                            Console.Write("Zelite li potvrditi uredivanje naziva? (da / ne) ");
                            if (Potvrda())
                            {
                                artikli[artikli.IndexOf(proizvod)] = noviProizvod;
                                Console.WriteLine("Uspješno uređen naziv artikla. ");
                  
                            }
                            break;
                        case "2":
                            pronadenoSvojstvo = true;
                            Console.Write("Unesi novu kolicinu za proizvod {0}: ", proizvod.naziv);
                            var novaKolicina = ProvjeraUnosaInt(0);
                            var noviProizvodKolicina = (proizvod.naziv, novaKolicina, proizvod.cijena, proizvod.datumIstekaRoka);
                            Console.Write("Zelite li potvrditi uredivanje kolicine? (da / ne) ");
                            if (Potvrda())
                            {
                                artikli[artikli.IndexOf(proizvod)] = noviProizvodKolicina;
                                Console.WriteLine("Uspješno uređena kolicina artikla. ");

                            }
                            break;
                        case "3":
                            pronadenoSvojstvo = true;
                            Console.Write("Unesi novu cijenu za proizvod {0}: ", proizvod.naziv);
                            var novaCijena = ProvjeraUnosaFloat(0);
                            var noviProizvodCijena = (proizvod.naziv, proizvod.kolicina, novaCijena, proizvod.datumIstekaRoka);
                            Console.Write("Zelite li potvrditi uredivanje cijene? (da / ne) ");
                            if (Potvrda())
                            {
                                artikli[artikli.IndexOf(proizvod)] = noviProizvodCijena;
                                Console.WriteLine("Uspješno uređena cijena artikla. ");

                            }
                            break;
  
                        case "4":
                            Console.WriteLine("Novi podaci datuma isteka roka za proizvod {0}.", proizvod.naziv);
                            Console.Write("Unesi novu godinu isteka roka: ");
                            var godina = ProvjeraUnosaInt(0);
                            Console.Write("Unesi novi mjesec isteka roka: ");
                            var mjesec = UnosRasponaIntegera(0, 13);
                            Console.Write("Unesi novi dan isteka roka: ");
                            int dan;
                            switch (mjesec)
                            {
                                case 1:
                                case 3:
                                case 5:
                                case 7:
                                case 8:
                                case 10:
                                case 12:
                                    dan = UnosRasponaIntegera(1, 32);
                                    break;
                                case 4:
                                case 6:
                                case 9:
                                case 11:
                                    dan = UnosRasponaIntegera(1, 31);
                                    break;
                                default:
                                    dan = UnosRasponaIntegera(1, 29);
                                    break;
                            }
                            Console.Write("Zelite li potvrditi uredivanje datuma isteka roka? (da / ne) ");
                            var noviProizvodDatum = (proizvod.naziv, proizvod.kolicina, proizvod.cijena, new DateTime(godina, mjesec, dan));
                            if (Potvrda())
                            {
                                artikli[artikli.IndexOf(proizvod)] = noviProizvodDatum;
                                Console.WriteLine("Uspješno uređen datum isteka roka artikla. ");

                            }
                            pronadenoSvojstvo = true;
                            break;
                        default:
                            Console.Write("\nIzbor nepostoji.\nPonovi unos: ");
                            break;
                    }
                    Console.WriteLine("\nStisni ENTER za drugi unos.");
                    Console.ReadLine();
                }               
            }

        }

        if (!artiklPronaden && !urediDrugiArtikl)
        {
            Console.Write("Nepostoji taj artikl u bazi. \nZelite li pokušati ponovno unijeti? (da / ne) ");
            artiklPronaden = !Potvrda();
        }
    }
    return artikli;
}
static List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> UredivanjeArtiklaPopust(List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikli)
{
    Console.Clear();
    Console.WriteLine("Artikli > Uređivanje Artikla > Popust ili poskupljenje svih proizvoda\n");

    Console.WriteLine("Želiš li poskupljenje ili popust? ");
    Console.WriteLine("1 - Poskupljenje \n2 - Popust \n0 - Poništi promjene i vrati se natrag");
    var pronadenOdgovor = false;
    var namjena = "";
    Console.Write("Unos broja: ");
    while (!pronadenOdgovor)
    {
        switch (Console.ReadLine())
        {
            case "0":
                pronadenOdgovor = true;
                break;
            case "1":
                pronadenOdgovor = true;
                namjena = "poskupljenje";
                break;
            case "2":
                pronadenOdgovor = true;
                namjena = "popust";
                break;
            default:
                Console.WriteLine("Neispravno uneseni podaci.\nUnesite ponovo: ");
                Console.Write("Ponovite unos: ");
                break;
        }
    }

    var raspon = "(1 - 99)";
    int postotak = 1;
    if(namjena == "poskupljenje")
    {
        Console.Write("Unesi za koliki postotak želiš poskupiti sve proizvode {0}: ", raspon);
        postotak = -UnosRasponaIntegera(1, 100);
    }
    else
    {
        if(namjena == "popust")
        {
            Console.Write("Unesi za koliki postotak želiš popust sve proizvode {0}: ", raspon);
            postotak = UnosRasponaIntegera(1, 100);
        }
    }

    if (namjena != "" )
    {
        Console.Write("Potvrdujete li promjene u cjenama artikla? (da / ne)");
        if (Potvrda())
        {
            for (int i = 0; i < artikli.Count; i++)
            {
                float KolicinaPostotka = (float)((int)((float)(artikli[i].cijena * (float)postotak / 100f) * 100)) / 100;
                artikli[i] = (artikli[i].naziv, artikli[i].kolicina, ((float)((int)((artikli[i].cijena - KolicinaPostotka) * 100)) / 100), artikli[i].datumIstekaRoka);
            }
            Console.WriteLine("Promjene u cijeni su postavljene. ");
        }
        else
        {
            Console.WriteLine("Promjene u cijeni su ponistene. ");
        }
    }

    
    Console.WriteLine("\nStisni ENTER za drugi unos.");
    Console.ReadLine();
    return artikli;

}
static void IspisArtikla(List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikli)
{
    Console.WriteLine("\nNAZIV (KOLICINA) - CIJENA - DANA DO ISTEKA ROKA");
    foreach ((string naziv, int kolicina, float cijena, DateTime datumIstekaRoka) proizvod in artikli)
    {
        var cijenaString = "";
        if((proizvod.cijena * 100) % 100 == 0)
        {
            cijenaString = $"{proizvod.cijena}.00e";
        }
        else if((proizvod.cijena * 100) % 10 == 0)
        {
            cijenaString = $"{proizvod.cijena}0e";
        }
        else
        {
            cijenaString = $"{proizvod.cijena}e";
        }

        var doIsteka = DoIsteka(proizvod.datumIstekaRoka);

        Console.WriteLine($"{proizvod.naziv} ({proizvod.kolicina}) - {cijenaString} - {doIsteka}");
    }
}

static void IspisArtiklaIme(List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikliKopija)
{
    Console.WriteLine("\nNAZIV (KOLICINA) - CIJENA - DATUM ISTEKA ROKA");
    var artikli = new List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)>();
    foreach(var proizvodi in artikliKopija)
    {
        artikli.Add(proizvodi);
    }

    artikli.Sort();
    foreach ((string naziv, int kolicina, float cijena, DateTime datumIstekaRoka) proizvod in artikli)
    {
        var cijenaString = "";
        if ((proizvod.cijena * 100) % 100 == 0)
        {
            cijenaString = $"{proizvod.cijena}.00e";
        }
        else if ((proizvod.cijena * 100) % 10 == 0)
        {
            cijenaString = $"{proizvod.cijena}0e";
        }
        else
        {
            cijenaString = $"{proizvod.cijena}e";
        }


        Console.WriteLine($"{proizvod.naziv} ({proizvod.kolicina}) - {cijenaString} - {proizvod.datumIstekaRoka}");
    }
}
static void IspisArtiklaDatum(List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikli, bool silazno)
{
    var najmanjaGodina = artikli[0].datumIstekaRoka.Year;
    var artikliKopija = new List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)>();
    foreach (var proizvod in artikli)
    {
        if(proizvod.datumIstekaRoka.Year < najmanjaGodina)
        {
            najmanjaGodina = proizvod.datumIstekaRoka.Year;
        }
        artikliKopija.Add(proizvod);
    }

    
    for(int i = 0; i < artikliKopija.Count ; i++)
    {
        for (int j = 0; j < artikliKopija.Count - 1; j++)
        {
            var razlikaGodina1 = artikliKopija[j].datumIstekaRoka.Year - najmanjaGodina;
            var dani1 = razlikaGodina1* 365 + artikliKopija[j].datumIstekaRoka.DayOfYear;
       

            var razlikaGodina2 = artikliKopija[j + 1].datumIstekaRoka.Year - najmanjaGodina;
            var dani2 = razlikaGodina2 * 365 + artikliKopija[j + 1].datumIstekaRoka.DayOfYear;
            if (dani2 > dani1)
            {
                var temp = artikliKopija[j + 1];
                artikliKopija[j + 1] = artikliKopija[j];
                artikliKopija[j] = temp;
            }

        }
       
    }
    if (!silazno)
    {
        artikliKopija.Reverse();
    }

    Console.WriteLine("\nNAZIV (KOLICINA) - CIJENA - DATUM ISTEKA ROKA");
    foreach (var proizvod in artikliKopija)
    {

        var cijenaString = "";
        if ((proizvod.cijena * 100) % 100 == 0)
        {
            cijenaString = $"{proizvod.cijena}.00e";
        }
        else if ((proizvod.cijena * 100) % 10 == 0)
        {
            cijenaString = $"{proizvod.cijena}0e";
        }
        else
        {
            cijenaString = $"{proizvod.cijena}e";
        }
        Console.WriteLine($"{proizvod.naziv} ({proizvod.kolicina}) - {cijenaString} - {proizvod.datumIstekaRoka}");
    }
}

static void IspisArtiklaKolicina(List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikli)
{
    var artikliKopija = new List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)>();
    foreach (var proizvod in artikli)
    {
        artikliKopija.Add(proizvod);
    }


    for (int i = 0; i < artikliKopija.Count; i++)
    {
        for (int j = 0; j < artikliKopija.Count - 1; j++)
        {
            if (artikliKopija[j].kolicina  > artikliKopija[j + 1].kolicina)
            {
                var temp = artikliKopija[j + 1];
                artikliKopija[j + 1] = artikliKopija[j];
                artikliKopija[j] = temp;
            }
        }
    }

    Console.WriteLine("\nNAZIV (KOLICINA) - CIJENA - DATUM ISTEKA ROKA");
    foreach (var proizvod in artikliKopija)
    {

        var cijenaString = "";
        if ((proizvod.cijena * 100) % 100 == 0)
        {
            cijenaString = $"{proizvod.cijena}.00e";
        }
        else if ((proizvod.cijena * 100) % 10 == 0)
        {
            cijenaString = $"{proizvod.cijena}0e";
        }
        else
        {
            cijenaString = $"{proizvod.cijena}e";
        }
        Console.WriteLine($"{proizvod.naziv} ({proizvod.kolicina}) - {cijenaString} - {proizvod.datumIstekaRoka}");
    }
}

static List<(string naziv, int kolicina)> NajviseProdavanArtikl(Dictionary<int, (DateTime vrijemeIzdavanja, List<(string naziv, int kolicina)> proizvodi)> racuni)
{
    var listaProdanihProizvoda = new List<(string naziv, int kolicina)>();
    foreach (var racun in racuni)
    {
        foreach (var proizvod in racun.Value.proizvodi)
        {
            var pronaden = false;

            for (int i = 0; i < listaProdanihProizvoda.Count; i++)
            {
                if (listaProdanihProizvoda[i].naziv == proizvod.naziv)
                {
                    listaProdanihProizvoda[i] = (proizvod.naziv, proizvod.kolicina + listaProdanihProizvoda[i].kolicina);
                    pronaden = true;
                }

            }

            if (!pronaden)
            {
                listaProdanihProizvoda.Add(proizvod);
            }
        }
    }


    var najviseProdavanArtikl = new List<(string naziv, int kolicina)> { listaProdanihProizvoda[0] };
    foreach(var proizvod in listaProdanihProizvoda)
    {
        if(proizvod.kolicina > najviseProdavanArtikl[0].kolicina)
        {
            najviseProdavanArtikl = new List<(string naziv, int kolicina)>() { proizvod };
        }
        else
        {
            if (proizvod.kolicina == najviseProdavanArtikl[0].kolicina && proizvod.naziv != najviseProdavanArtikl[0].naziv)
            {
                najviseProdavanArtikl.Add(proizvod);
            }
        }
    }
    return najviseProdavanArtikl;
}

static List<(string naziv, int kolicina)> NajmanjeProdavanArtikl(Dictionary<int, (DateTime vrijemeIzdavanja, List<(string naziv, int kolicina)> proizvodi)> racuni, List<(string naziv, int kolicina, float cijena, DateTime datumIstekaRoka)> artikli)
{
    var listaProdanihProizvoda = new List<(string naziv, int kolicina)>();
    foreach(var racun in racuni)
    {
        foreach(var proizvod in racun.Value.proizvodi)
        {
            var pronaden = false;

            for (int i = 0; i< listaProdanihProizvoda.Count; i++)
            {
                if (listaProdanihProizvoda[i].naziv == proizvod.naziv)
                {
                    listaProdanihProizvoda[i] = (proizvod.naziv, proizvod.kolicina + listaProdanihProizvoda[i].kolicina);
                    pronaden = true;
                }

            }

            if (!pronaden)
            {
                listaProdanihProizvoda.Add(proizvod);
            }
        }
    }

    foreach (var proizvod in artikli)
    {
         
        var pronaden = false;
        foreach (var proizvodLista in listaProdanihProizvoda)
        {
            if (proizvodLista.naziv == proizvod.naziv) pronaden = true;
        }
        if (!pronaden) listaProdanihProizvoda.Add((proizvod.naziv, kolicina: 0));
    }

    var najmanjeProdavanArtikl = new List<(string naziv, int kolicina)> { listaProdanihProizvoda[0] };
    foreach(var proizvodLista in listaProdanihProizvoda)
    {
        if(proizvodLista.kolicina < najmanjeProdavanArtikl[0].kolicina)
        {
            najmanjeProdavanArtikl = new List<(string naziv, int kolicina)>() { proizvodLista };
        }
        else
        {
            if(proizvodLista.kolicina == najmanjeProdavanArtikl[0].kolicina)
            {
                najmanjeProdavanArtikl.Add(proizvodLista);
            }
        }
    }
  
    return najmanjeProdavanArtikl;
}

static List<(string imePrezime, DateTime rodenje)> UnosRadnika(List<(string imePrezime, DateTime rodenje)> radnici)
{
    Console.Clear();
    Console.WriteLine("Radnici > Unos Radnika \n");
    var prosliRadnici = new List<(string imePrezime, DateTime rodenje)>();
    foreach (var radnik in radnici)
    {
        prosliRadnici.Add(radnik);
    }
    var unos = true;
    while (unos)
    {
        Console.Write("Unesi ime i prezime novog radnika: ");
        var nazivOsobe = Console.ReadLine().ToLower();

        Console.Write("\nUnesi godinu rodenja: ");
        var godina = ProvjeraUnosaInt(0);
        Console.Write("\nUnesi mjesec rodenja: ");
        var mjesec = UnosRasponaIntegera(0, 13);
        Console.Write("\nUnesi dan rodenja: ");
        int dan;
        switch (mjesec)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                dan = UnosRasponaIntegera(1, 32);
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                dan = UnosRasponaIntegera(1, 31);
                break;
            default:
                dan = UnosRasponaIntegera(1, 29);
                break;
        }

        radnici.Add((imePrezime: nazivOsobe, rodenje: new DateTime(godina, mjesec, dan)));

        Console.Write("\nZelite li unijeti jos jednog radnika? (da / ne) ");
        unos = Potvrda();
    }

    Console.Write("\nPotvrđujete li ove promjene? (da / ne) ");
    var potvda = Potvrda();
    if (potvda)
    {
        return radnici;
    }
    else
    {
        return prosliRadnici;
    }

    return radnici;
}

static void BrisanjeRadnika(List<(string imePrezime, DateTime rodenje)> radnici)
{
    Console.Clear();
    Console.WriteLine("Radnici > Brisanje Radnika > Po imenu\n");
    var radnikPronaden = false;
    string unos;
    while (!radnikPronaden)
    {
        Console.Write("Unesi ime i prezime radnika kojeg želiš izbrisati: ");
        unos = Console.ReadLine().ToLower();

        for (int i = 0; i < radnici.Count; i++)
        {
            if (radnici[i].imePrezime == unos && !radnikPronaden)
            {
                Console.Write("Jeste li sigurni da želite izbrisati ovag radnika? (da / ne) ");
                if (Potvrda())
                {
                    radnici.Remove(radnici[i]);
                    Console.WriteLine("radnik {0} uspjesno izbrisan. ", unos);
                }

                i = radnici.Count - 1;
                radnikPronaden = true;
                Console.WriteLine("Stisnite ENTER za drugi unos.");
                Console.ReadLine();
            }
        }

        if (!radnikPronaden)
        {
            Console.Write("Nepostoji taj radnik u bazi. \nZelite li pokušati ponovno unijeti? (da / ne) ");
            radnikPronaden = !Potvrda();
        }
    }
}
static void BrisanjeRadniciGodine(List<(string imePrezime, DateTime rodenje)> radnici)
{
    Console.Write("Zelite li potvriti brisanje svih radnika koji imaju vise od 65 godina? (da / ne)");
    var potvrda = Potvrda();
    if (potvrda)
    {
        for (int i = 0; i < radnici.Count; i++)
        {
            if (Godine(radnici[i].rodenje) > 65)
            {
                radnici.Remove(radnici[i]);
            }
        }
    }
    Console.WriteLine("\nStisni ENTER za drugi unos.");
    Console.ReadLine();
}

static List<(string imePrezime, DateTime rodenje)> UredivanjeRadnika(List<(string imePrezime, DateTime rodenje)> radnici)
{
    Console.Clear();
    Console.WriteLine("Radnici > Uredivanje Radnika\n");
    var radnikPronaden = false;
    string unos;
    while (!radnikPronaden)
    {
        Console.Write("\nUnesi ime i prezime radnika kojeg želiš urediti: ");
        unos = Console.ReadLine().ToLower();
        var urediDrugogRadnika = false;
        for (int i = 0; i < radnici.Count; i++)
        {
            if (radnici[i].imePrezime == unos && !radnikPronaden)
            {
                radnikPronaden = true;
                var radnik = radnici[i];
                i = radnici.Count - 1;
                Console.WriteLine("\nKoje svojstvo radnika {0} želite promijeniti? ", unos);
                Console.WriteLine("1 - Ime i prezime\n2 - Datum Rodenja\n0 -ponisti uredivanje i izaberi drugog radnika za urediti");
                Console.Write("Unesi broj: ");
                var pronadenoSvojstvo = false;
                while (!pronadenoSvojstvo)
                {
                    switch (Console.ReadLine())
                    {
                        case "0":
                            Console.WriteLine("Vise ne uredujete radnika {0}.", unos);
                            radnikPronaden = false;
                            urediDrugogRadnika = true;
                            pronadenoSvojstvo = true;
                            break;
                        case "1":
                            pronadenoSvojstvo = true;
                            Console.Write("Unesi novo ime i pretime za radnika{0}: ", radnik.imePrezime);
                            var noviRadnik = (Console.ReadLine().ToLower(), radnik.rodenje);
                            Console.Write("Zelite li potvrditi uredivanje naziva? (da / ne) ");
                            if (Potvrda())
                            {
                                radnici[radnici.IndexOf(radnik)] = noviRadnik;
                                Console.WriteLine("Uspješno uređeno ime i prezime radnika. ");

                            }
                            break;
                        case "2":
                            pronadenoSvojstvo = true;

                            Console.WriteLine("Novi podaci datuma isteka roka za proizvod {0}.", radnik.imePrezime);
                            Console.Write("Unesi novu godinu rodenja: ");
                            var godina = ProvjeraUnosaInt(0);
                            Console.Write("Unesi novi mjesec rodenja: ");
                            var mjesec = UnosRasponaIntegera(0, 13);
                            Console.Write("Unesi novi dan rodenja: ");
                            int dan;
                            switch (mjesec)
                            {
                                case 1:
                                case 3:
                                case 5:
                                case 7:
                                case 8:
                                case 10:
                                case 12:
                                    dan = UnosRasponaIntegera(1, 32);
                                    break;
                                case 4:
                                case 6:
                                case 9:
                                case 11:
                                    dan = UnosRasponaIntegera(1, 31);
                                    break;
                                default:
                                    dan = UnosRasponaIntegera(1, 29);
                                    break;
                            }
                            Console.Write("Zelite li potvrditi uredivanje datuma rodenja? (da / ne) ");
                            var noviRadnikDatum = (radnik.imePrezime, new DateTime(godina, mjesec, dan));
                            if (Potvrda())
                            {
                                radnici[radnici.IndexOf(radnik)] = noviRadnikDatum;
                                Console.WriteLine("Uspješno uređen datum rodenja osobe. ");

                            }
                            break;
                        default:
                            Console.Write("\nIzbor nepostoji.\nPonovi unos: ");
                            break;
                    }
                    Console.WriteLine("\nStisni ENTER za drugi unos.");
                    Console.ReadLine();
                }
            }

        }

        if (!radnikPronaden && !urediDrugogRadnika)
        {
            Console.Write("Nepostoji taj radnik u bazi. \nZelite li pokušati ponovno unijeti? (da / ne) ");
            radnikPronaden = !Potvrda();
        }
    }
    return radnici;
}

static void IspisRadnika(List<(string imePrezime, DateTime rodenje)> radnici)
{
        Console.WriteLine("\nIME - GODINE");
        foreach (var radnik in radnici)
        {
            var godine = Godine(radnik.rodenje);
            Console.WriteLine($"{radnik.imePrezime} - {godine}");
        }
}
static void IspisRadnikaTekuciMjesec(List<(string imePrezime, DateTime rodenje)> radnici)
{
    Console.WriteLine("\nIME - DATUM RODENJA");
    foreach (var radnik in radnici)
    {
        if(DateTime.Now.Month == radnik.rodenje.Month)
        {
            Console.WriteLine($"{radnik.imePrezime} - {radnik.rodenje}");
        }
    }
}
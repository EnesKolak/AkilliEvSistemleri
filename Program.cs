using System;
using System.Collections.Generic;

enum CihazTuru
{
    Lamba, Klima, GuvenlikSistemi
}

class Cihaz
{
    private string _kimlik;
    private string _ad;
    private string _turu;
    private bool _acikMi;
    public string Kimlik { get; private set; }
    public string Ad { get; private set; }
    public CihazTuru CihazTuru { get; private set; }
    public bool AcikMi { get; private set; }

    public Cihaz(string kimlik, string ad, CihazTuru tur)
    {
        Kimlik = kimlik;
        Ad = ad;
        CihazTuru = tur;
        AcikMi = false;
    }

    public void Ac()
    {
        AcikMi = true;
    }

    public void Kapat()
    {
        AcikMi = false;
    }

    public void BilgileriGoster()
    {
        string durum = AcikMi ? "Acik" : "Kapali";
        Console.WriteLine($"Cihaz Kimligi: {Kimlik}");
        Console.WriteLine($"Cihaz Adi: {Ad}");
        Console.WriteLine($"Cihaz Turu: {CihazTuru}");
        Console.WriteLine($"Cihaz Acik Mi?: {durum}");
    }
}

namespace MyNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cihaz> cihazlar = new List<Cihaz>
            {
                new Cihaz("C01", "Salon Lambasi", CihazTuru.Lamba),
                new Cihaz("C02", "Oturma Odasi Klimasi", CihazTuru.Klima),
                new Cihaz("C03", "Giris Kapi Alarmi", CihazTuru.GuvenlikSistemi)
            };
            int secim;

            do
            {
                Console.WriteLine("----- Akilli Ev Yonetim Sistemi -----");
                Console.WriteLine();
                Console.WriteLine("1- Listele");
                Console.WriteLine("2- Yeni Cihaz Ekle");
                Console.WriteLine("3- Cihaz Ac");
                Console.WriteLine("4- Cihaz Kapat");
                Console.WriteLine("0- Cikis");
                Console.Write("Seciminiz: ");

                if (!int.TryParse(Console.ReadLine(), out secim))
                {
                    Console.WriteLine("Lutfen gecerli bir sayi giriniz!");
                    secim = -1;
                    continue;
                }

                switch (secim)
                {
                    case 1:
                        if (cihazlar.Count == 0)
                        {
                            Console.WriteLine("Liste bos!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("----- Cihaz Bilgisi -----");
                            Console.WriteLine();

                            foreach (var cihaz in cihazlar)
                            {
                                cihaz.BilgileriGoster();
                                Console.WriteLine();
                            }
                        }

                        break;
                    case 2:
                        Console.Write("Eklenecek Cihaz Kimligi: ");
                        string tempKimlik = Console.ReadLine();

                        if (CihazBul(cihazlar, tempKimlik) != null)
                        {
                            Console.WriteLine("Bu kimlige sahip bir cihaz zaten mevcut!");
                            break;
                        }

                        Console.Write("Eklenecek Cihaz Adi: ");
                        string tempAd = Console.ReadLine();

                        Console.WriteLine("Cihaz Turu Seciniz (0- Lamba, 1- Klima, 2- GuvenlikSistemi): ");
                        string turGiris = Console.ReadLine();

                        if (!Enum.TryParse(turGiris, true, out CihazTuru tempTur) || !Enum.IsDefined(typeof(CihazTuru), tempTur))
                        {
                            Console.WriteLine("HATA: Gecersiz cihaz turu girdiniz!");
                            break;
                        }

                        cihazlar.Add(new Cihaz(tempKimlik, tempAd, tempTur));
                        Console.WriteLine("Cihaz basariyla eklendi.");
                        break;

                    case 3:
                        Console.Write("Acilacak cihaz kimligini giriniz: ");
                        string acilacakKimlik = Console.ReadLine();
                        Cihaz acilacakCihaz = CihazBul(cihazlar, acilacakKimlik);

                        if (acilacakCihaz == null)
                        {
                            Console.WriteLine("HATA: Belirtilen kimlikte bir cihaz bulunamadi!");
                        }
                        else
                        {
                            acilacakCihaz.Ac();
                            Console.WriteLine($"{acilacakCihaz.Ad} acildi.");
                        }
                        break;

                    case 4:
                        Console.Write("Kapatilacak cihaz kimligini giriniz: ");
                        string kapatilacakKimlik = Console.ReadLine();
                        Cihaz kapatilacakCihaz = CihazBul(cihazlar, kapatilacakKimlik);

                        if (kapatilacakCihaz == null)
                        {
                            Console.WriteLine("HATA: Belirtilen kimlikte bir cihaz bulunamadi!");
                        }
                        else
                        {
                            kapatilacakCihaz.Kapat();
                            Console.WriteLine($"{kapatilacakCihaz.Ad} kapatildi.");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Sistem Kapaniyor!");
                        break;
                    default:
                        Console.WriteLine("Yanlis deger girdiniz! Lutfen tekrar deneyiniz!");
                        break;
                }
            }
            while (secim != 0);

            static Cihaz CihazBul(List<Cihaz> cihazlar, string kimlik)
            {
                foreach (var cihaz in cihazlar)
                {
                    if (cihaz.Kimlik == kimlik)
                    {
                        return cihaz;
                    }
                }
                return null;
            }
        }

    }

}

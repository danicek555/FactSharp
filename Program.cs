using System.Text.Json; //musíme přidat System.Text.Json aby to fungovalo na macu

class Program //specialni nazev tridy - po spusteni programu se spusti funkce Main() z tridy Program
{
    static void RestartProgram() //funkce pro opakování programu jako v javascriptu jenom tady static void
    //static void znamena ze funkce nema vratit nic, jen neco dela
    //void znamena ze funkce vrati nic
    //static znamena ze funkce patri do tridy a nemusime vytvaret instanci tridy
    //Program.Main() znamena ze se spusti funkce Main() z tridy Program

    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nChces znovu spustit program? (ano/ne)");
        Console.ResetColor(); //chci jen jeden radek barvu
        string volba = Console.ReadLine();
        if (volba == "ano")
        {
            Console.Clear();
            Main().GetAwaiter().GetResult();
            //GetAwaiter().GetResult() znamena ocekavani a vrateni vysledku
        }
    }

    static async Task Main()
        //async znamena ze funkce je asynchronni - proste kdyz mas await (stejne jako v javascriptu) tak musi byt async
        //Task neco jako Promise v javascriptu, program nemusi cekat na vysledek, ale muze pokracovat
        //tahle funkce se spusti hnedka po spusteni programu (hleda se funkce Main())
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Vítej v generátoru faktů");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Vtípek na úvod:");
        
        HttpClient klient = new HttpClient();

        string vtip = await klient.GetStringAsync("https://geek-jokes.sameerkumar.website/api?format=text");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(vtip);
      

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Možnosti: fakt o čísle, fakt o roce, fakt o datu, náhodný fakt, GJS, tvoje jmeno");
        Console.ResetColor();
        string volba = Console.ReadLine();

        Console.Clear();

        if (volba == "fakt o čísle")
        {
            Console.WriteLine("Napis: cislo  - pokud chces zadat svoje cislo, nahodne cislo -  pokud chces nahodne cislo");
            string volba2 = Console.ReadLine();
            if (volba2 == "cislo")
            {
                Console.WriteLine("Zadej cislo: ");
                string cislo = Console.ReadLine();

                string fakt = await klient.GetStringAsync("http://numbersapi.com/" + cislo);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fakt o číslu: " + fakt);
            }
            else if (volba2 == "nahodne cislo")
            {
                string fakt = await klient.GetStringAsync("http://numbersapi.com/random/");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fakt o nahodnem cisle: " + fakt);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Neznama volba");
            }
        }
        else if (volba == "fakt o roce")
        {
            Console.WriteLine("Napis: rok  - pokud chces tento rok, nahodny rok  -  pokud chces nahodny rok");
            string volba2 = Console.ReadLine();
            if (volba2 == "rok")
            {
                Console.WriteLine("Year: " + DateTime.Now.Year);
                string fakt = await klient.GetStringAsync("http://numbersapi.com/" + DateTime.Now.Year + "/year");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fakt o tomto roce: " + fakt);
            }
            else if (volba2 == "nahodny rok")
            {
                string fakt = await klient.GetStringAsync("http://numbersapi.com/random/year");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fakt o nahodnem roce: " + fakt);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Neznama volba");
            }
        }
        else if (volba == "fakt o datu")
        {
            string fakt = await klient.GetStringAsync("http://numbersapi.com/random/date");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fakt o nahodnem dni: " + fakt);
        }
        else if (volba == "náhodný fakt")
        {
            string fakt = await klient.GetStringAsync("http://numbersapi.com/random");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Náhodný fakt: " + fakt);
        }
        else if (volba == "GJS")
        {
            Console.WriteLine("GJS");
            string kod = await klient.GetStringAsync("https://gymjs.cz/");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(kod + "Celý kód stránky GJS (php)");
        }
        else if (volba == "tvoje jmeno")
        {

            Console.WriteLine("Zadej tvoje jmeno: ");

            string jmeno = Console.ReadLine();
            string fakt = await klient.GetStringAsync("https://api.agify.io/?name=" + jmeno);
    
            Console.WriteLine("Chces to v JSONU? (ano/ne)");

            string volba3 = Console.ReadLine();
            if (volba3 == "ano")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(fakt);
                Console.ResetColor();
            }
            else if (volba3 == "ne")
            {
                try
                {
                    JsonDocument doc = JsonDocument.Parse(fakt);

                    int count = doc.RootElement.GetProperty("count").GetInt32();
                    string name = doc.RootElement.GetProperty("name").GetString();
                    int age = doc.RootElement.GetProperty("age").GetInt32();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tvoje číslo: " + count);
                    Console.WriteLine("Tvoje jmeno: " + jmeno);
                    Console.WriteLine("Tvůj věk: " + age);
                  
                }
                catch (Exception ex) //chyta chybu kdyz se zpracovavani dat nepovede (stejne jako v javascriptu jen ze tady je (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Co jsi tam napsal za jmeno kamo? Zkus to znovu.");
                    Console.WriteLine($"Chyba: {ex.Message}");
                   
                }
            }
            else
            {  
               Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Neznama volba");
              
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Neznama volba");
    
        }

        RestartProgram();
    }
} 


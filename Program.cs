using App; // Kopplar in App-namespacet (Item, User, SaveData, TradeActions)

// ============================================================
// RASTALOGIN — Trading System (console)
// Viktigt: vi behåller menyvalen exakt som tidigare ("1"-"10" och "f")
// för att inte bryta förväntat beteende i resten av programmet.
// ============================================================

// Ladda data innan menyn visas (så allt bygger på aktuellt filinnehåll).
SaveData.LoadUsers();
SaveData.LoadItems();
SaveData.LoadTrades();

// Samlar all interaktionslogik i en klass → Program.cs hålls tunn & tydlig.
TradeActions actions = new TradeActions();

// Enkel körflagga för huvudloopen (lätt att följa, lätt att avsluta).
bool running = true;

while (running)
{
  // ========= stylized header =========
Console.Clear();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║           RASTALOGIN TRADING         ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.ResetColor();

Console.WriteLine("Welcome, bredren/sistren! Welcome to the Jamaican e-yardsale.");
Console.WriteLine();
Console.WriteLine("Pick one option below:");
Console.WriteLine();

// helper to print one menu line with a subtle accent
void Line(string key, string text)
{
  Console.ForegroundColor = ConsoleColor.DarkGray;
  Console.Write("  [" + key + "] ");
  Console.ForegroundColor = ConsoleColor.White;
  Console.WriteLine(text);
  Console.ResetColor();
}

// ========= menu (same keys!) =========
Line("1", "Register an account");
Line("2", "Log in");
Line("3", "Upload an item to trade");
Line("4", "Show other users' items");
Line("5", "Request a trade");
Line("6", "Show your trade requests");
Line("7", "Accept a trade request");
Line("8", "Deny a trade request");
Line("9", "Show completed trades");
Line("10", "Log out");
Console.ForegroundColor = ConsoleColor.Red;
Line("f", "Close (ditch the e-yardsale)");
Console.ResetColor();

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.DarkGray;
Console.Write("Your choice: ");
Console.ResetColor();


  // Vi läser som string för enkel jämförelse i switchen.
  string input = Console.ReadLine();

  switch (input)
  {
    case "1":
      actions.Registration(); // likkle onboarding
      break;

    case "2":
      actions.LogIn(); // open di gate
      break;

    case "3":
      actions.AddItem(); // add yuh goods
      break;

    case "4":
      actions.ShowOthersItems(); // window shopping
      break;

    case "5":
      actions.SendRequest(); // hail di owner
      break;

    case "6":
      actions.ShowRequests(); // who ping yuh
      break;

    case "7":
      actions.AcceptRequests(); // seal di deal
      break;

    case "8":
      actions.DenyRequest(); // not today, mi fren
      break;

    case "9":
      actions.ShowCompleted(); // done & dusted
      break;

    case "10":
      actions.LogOut(); // step out clean
      break;

    case "f":
      Console.Clear();
      running = false; // enkel och tydlig exit utan special-case
      break;

    default:
      // Robust felhantering: vi loopar tillbaka istället för att krascha.
      // (PirateSoftware, inte mer spaghetti här: clean loop, clean yard.)
      Console.Clear();
      Console.WriteLine("Wrong or empty choice, ya know? Try again.");
      Console.WriteLine("Press ENTER to continue.");
      Console.ReadLine();
      break;
  }
}

// Tydlig slutpunkt i konsolen (bra för manuell testning & demo).
Console.WriteLine("Session closed. Bless up and walk good, trader of the RASTALOGIN yard.");

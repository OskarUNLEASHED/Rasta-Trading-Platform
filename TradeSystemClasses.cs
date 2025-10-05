namespace App; // Håller ihop klassen med Program.cs och övriga typer

// Samlar interaktionslogiken (användare, prylar, trades).
public class TradeActions
{
  // Håller data i minnet; sparas till fil efter varje ändring.
  List<User> users = SaveData.LoadUsers();
  List<Item> items = SaveData.LoadItems();
  List<Trade> trades = SaveData.LoadTrades();

  // Vem är inloggad just nu?
  User? active_user = null;

  // Tillfälliga inmatningar vid inloggning.
  string? useremail;
  string? password;

  //  Registration
  // Sparar direkt → minskar risk att tappa data vid avslut.
  public void Registration()
  {
    Console.Clear();
    Console.WriteLine("Drop yuh email fi register: ");
    string? useremail = Console.ReadLine();
    Console.WriteLine("Set yuh secret password: ");
    string? password = Console.ReadLine();

    if (useremail != "" && useremail != null && password != "" && password != null)
    {
      users.Add(new User(useremail, password));
      SaveData.SaveUsers(users);
      Console.WriteLine("Bless up! Account registered successfully.");
      Console.WriteLine();
    }
    else
    {
      Console.WriteLine("Email or password empty.");
      Console.WriteLine("Press ENTER and try again.");
      Console.ReadLine();
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  //Log In
  // Enkel linjär sökning i minnet duger för liten datamängd.
  public void LogIn()
  {
    Console.Clear();

    if (active_user == null)
    {
      Console.WriteLine("Your email:");
      useremail = Console.ReadLine();
      Console.Clear();

      Console.Write("Password: ");
      password = Console.ReadLine();

      User? found_user = null;

      foreach (User user in users)
      {
        if (user.TryLogin(useremail, password))
        {
          found_user = user;
          break;
        }
      }

      if (found_user != null)
      {
        active_user = found_user;
        Console.WriteLine($"Welcome inside the yard, {active_user.GetUserEmail()}!");
      }
      else
      {
        Console.WriteLine("Invalid email or password!");
      }
    }
    else
    {
      Console.WriteLine("You already logged in.");
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  //  Add Item 
  // Kopplar item till aktiv användare och sparar direkt.
  public void AddItem()
  {
    Console.Clear();

    if (active_user == null)
    {
      Console.WriteLine("Log in first.");
      Console.ReadLine();
    }
    else
    {
      Console.WriteLine("Item name:");
      string? itemname = Console.ReadLine();

      Console.WriteLine("Item description:");
      string? itemdescription = Console.ReadLine();

      string? owneremail = active_user.GetUserEmail();

      items.Add(new Item(itemname, itemdescription, owneremail));
      SaveData.SaveItems(items);

      Console.WriteLine("Your item is uploaded — smooth sailing.");
      Console.WriteLine();
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  //  Show Others' Items 
  // Filtrerar bort egna prylar så man inte förfrågar sig själv.
  public void ShowOthersItems()
  {
    Console.Clear();

    if (active_user == null)
    {
      Console.WriteLine("Log in first.");
    }
    else
    {
      Console.WriteLine("Items from other users:");
      foreach (Item item in items)
      {
        if (item.GetOwnerEmail() != active_user.GetUserEmail())
        {
          Console.WriteLine($"Item: {item.GetItemName()}, Description: {item.GetItemDescription()}, Owner: {item.GetOwnerEmail()}");
          Console.WriteLine("---------------------------------");
        }
      }
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  // Send Request 
  // Identifierar item via exakt namn + ägarens e-post (enkel textfilslösning).
  public void SendRequest()
  {
    Console.Clear();

    if (active_user == null)
    {
      Console.WriteLine("Log in first.");
    }
    else
    {
      ShowOthersItems();
      Console.WriteLine();
      Console.WriteLine("Type the exact item name:");
      string? itemname = Console.ReadLine();

      Console.WriteLine("Type the owner's email:");
      string? reciveremail = Console.ReadLine();

      Item? requested_item = null;

      foreach (Item item in items)
      {
        if (item.GetItemName() == itemname && item.GetOwnerEmail() == reciveremail)
        {
          requested_item = item;
          break;
        }
      }

      if (requested_item != null)
      {
        trades.Add(new Trade(active_user.GetUserEmail(), reciveremail, requested_item, Trade_Status.Pending));
        SaveData.SaveTrades(trades);
        Console.WriteLine("Request sent — response will reach you.");
      }
      else
      {
        Console.WriteLine("Item name or owner email not matching, check again.");
      }
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  // Show Requests (Pending) 
  public void ShowRequests()
  {
    Console.Clear();

    if (active_user == null)
    {
      Console.WriteLine("Log in first.");
    }
    else
    {
      Console.WriteLine("You received the following requests:");
      foreach (Trade trade in trades)
      {
        if (trade.GetReceiverEmail() == active_user.GetUserEmail()
            && trade.GetStatus() == Trade_Status.Pending)
        {
          Item it = trade.GetItemName(); // behåller kompatibelt namn
          Console.WriteLine($"from: {trade.GetSenderEmail()} , item: {it.GetItemName()}, status: {trade.GetStatus()}");
          Console.WriteLine("-------------------------------");
        }
      }
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  // Accept Requests
  public void AcceptRequests()
  {
    Console.Clear();

    if (active_user == null)
    {
      Console.WriteLine("Log in first.");
    }
    else
    {
      Console.WriteLine("You have requests from:");
      foreach (Trade trade in trades)
      {
        Console.WriteLine(trade.GetSenderEmail());
      }

      Console.WriteLine();
      Console.WriteLine("Enter the sender's email address:");
      string? senderemail = Console.ReadLine();

      foreach (Trade trade in trades)
      {
        if (trade.GetReceiverEmail() == active_user.GetUserEmail()
            && trade.GetSenderEmail() == senderemail
            && trade.GetStatus() == Trade_Status.Pending)
        {
          trade.Accept();
          SaveData.SaveTrades(trades);
          Console.WriteLine("Trade request accepted.");
        }
      }
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  //  Deny Request 
  public void DenyRequest()
  {
    Console.Clear();

    if (active_user == null)
    {
      Console.WriteLine("Log in first.");
    }
    else
    {
      Console.WriteLine("You have requests from:");
      foreach (Trade trade in trades)
      {
        Console.WriteLine(trade.GetSenderEmail());
      }

      Console.WriteLine();
      Console.WriteLine("Enter the sender's email address:");
      string? senderemail = Console.ReadLine();

      foreach (Trade trade in trades)
      {
        if (trade.GetReceiverEmail() == active_user.GetUserEmail()
            && trade.GetSenderEmail() == senderemail
            && trade.GetStatus() == Trade_Status.Pending)
        {
          trade.Deny();
          SaveData.SaveTrades(trades);
          Console.WriteLine("Trade request denied.");
        }
      }
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  // Show Completed Trades
  public void ShowCompleted()
  {
    Console.Clear();

    if (active_user == null)
    {
      Console.WriteLine("Log in first.");
    }
    else
    {
      foreach (Trade trade in trades)
      {
        if (trade.GetReceiverEmail() == active_user.GetUserEmail()
            && trade.GetStatus() == Trade_Status.Accepted)
        {
          Item it = trade.GetItemName();
          Console.WriteLine("You have the following completed trade:");
          Console.WriteLine($"from: {trade.GetSenderEmail()}");
          Console.WriteLine($"Item: {it.GetItemName()}");
          Console.WriteLine($"Description: {it.GetItemDescription()}");
          Console.WriteLine($"Status: {trade.GetStatus()}");
          Console.WriteLine("---------------------------");
        }
      }
    }

    Console.WriteLine("Press ENTER to continue...");
    Console.ReadLine();
  }

  // Log Out 
  // Vi nollställer endast sessionen; listor ligger kvar i minnet.
  public void LogOut()
  {
    Console.Clear();
    active_user = null;
    Console.WriteLine("Logged out. Give thanks for passing through.");
    Console.WriteLine();
  }
}

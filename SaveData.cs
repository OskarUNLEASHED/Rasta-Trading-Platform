using System;
using System.IO;

namespace App;

// SaveData: hanterar läsning och skrivning av användare, prylar och bytesförfrågningar.
// Vi håller det enkelt och textbaserat eftersom kursens nivå inte kräver databas ännu.
class SaveData
{
  // =========================
  // USERS
  // =========================

  // Sparar alla användare i users.txt.
  // Vi skriver över filen varje gång (append: false) för att hålla datan fräsch och undvika dubbletter.
  public static void SaveUsers(List<User> users)
  {
    using (StreamWriter writer = new StreamWriter("users.txt", append: false))
    {
      foreach (User user in users)
      {
        writer.WriteLine(user.GetUserEmail() + ',' + user.GetPassword());
      }
    }
  }

  // Läser in användare från users.txt.
  // Om filen inte finns returnerar vi en tom lista – på så sätt kraschar inte programmet vid första körningen.
  public static List<User> LoadUsers()
  {
    List<User> users = new List<User>();

    if (!File.Exists("users.txt")) return users;

    using (StreamReader reader = new StreamReader("users.txt"))
    {
      string? line; // nullable, eftersom ReadLine() kan returnera null
      while ((line = reader.ReadLine()) != null)
      {
        string[] parts = line.Split(',');
        if (parts.Length == 2)
        {
          // Vi litar på att filen är korrekt formaterad (inga extra fält).
          users.Add(new User(parts[0], parts[1]));
        }
      }
    }
    return users;
  }

  // =========================
  // ITEMS
  // =========================

  // Sparar alla prylar (items) i items.txt.
  // Samma princip – ny fil varje gång för att garantera konsekvent struktur.
  public static void SaveItems(List<Item> items)
  {
    using (StreamWriter writer = new StreamWriter("items.txt", append: false))
    {
      foreach (Item item in items)
      {
        writer.WriteLine(item.GetItemName() + ',' + item.GetItemDescription() + ',' + item.GetOwnerEmail());
      }
    }
  }

  // Läser in alla prylar från items.txt.
  // VARFÖR: Detta gör att varje start laddar den senaste versionen från fil, vilket passar bra i en enkel konsolapp.
  public static List<Item> LoadItems()
  {
    List<Item> items = new List<Item>();

    if (!File.Exists("items.txt")) return items;

    using (StreamReader reader = new StreamReader("items.txt"))
    {
      string? line;
      while ((line = reader.ReadLine()) != null)
      {
        string[] parts = line.Split(',');
        if (parts.Length == 3)
        {
          items.Add(new Item(parts[0], parts[1], parts[2]));
        }
      }
    }
    return items;
  }

  // =========================
  // TRADES
  // =========================

  // Sparar alla trade-förfrågningar till trades.txt.
  // Vi sparar i CSV-stil eftersom den är enkel att läsa manuellt om något går fel.
  public static void SaveTrades(List<Trade> trades)
  {
    using (StreamWriter writer = new StreamWriter("trades.txt", append: false))
    {
      foreach (Trade trade in trades)
      {
        // OBS: metoden heter GetItemName men returnerar hela Item-objektet – vi rör inte den för bakåtkompatibilitet.
        Item item = trade.GetItemName();
        writer.WriteLine(
          $"{trade.GetReceiverEmail()},{trade.GetSenderEmail()},{item.GetItemName()},{item.GetItemDescription()},{item.GetOwnerEmail()},{trade.GetStatus()}"
        );
      }
    }
  }

  // Läser in trades från trades.txt.
  // Här återskapar vi både Item och Trade-objekten baserat på sparade fält.
  public static List<Trade> LoadTrades()
  {
    List<Trade> trades = new List<Trade>();

    if (!File.Exists("trades.txt")) return trades;

    using (StreamReader reader = new StreamReader("trades.txt"))
    {
      string? line;
      while ((line = reader.ReadLine()) != null)
      {
        string[] parts = line.Split(',');
        if (parts.Length == 6)
        {
          string receiverEmail = parts[0];
          string senderEmail = parts[1];

          // BOMBACLAT! Här återskapas själva varan i traden.
          Item item = new Item(parts[2], parts[3], parts[4]);

          // Enum.Parse används för att läsa in statusen (Pending/Accepted/Denied) som text.
          Trade_Status status = (Trade_Status)Enum.Parse(typeof(Trade_Status), parts[5]);

          trades.Add(new Trade(senderEmail, receiverEmail, item, status));
        }
      }
    }
    return trades;
  }
}

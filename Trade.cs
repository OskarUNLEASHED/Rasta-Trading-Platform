namespace App;

// Enum för status på en trade.
// Vi använder en enum här för att tydligt skilja på olika lägen (Pending, Accepted, Denied)
// istället för att jobba med osäkra strängar.
public enum Trade_Status
{
  Pending,
  Accepted,
  Denied
}

// Klassen Trade representerar en bytesförfrågan mellan två användare.
// Den binder ihop vem som skickade, vem som tar emot, vilken vara det gäller och vilket läge bytet har.
public class Trade
{
  private string? SenderEmail;     // Avsändarens e-post (den som skickar förfrågan)
  private string? ReceiverEmail;   // Mottagarens e-post (den som får förfrågan)
  private Item TradeItemName;      // Själva varan i bytet
  private Trade_Status Status;     // Status för bytet (Pending, Accepted, Denied)

  // Konstruktor — sätter allt direkt vid skapandet.
  // Vi håller parameterordningen konsekvent med hur SaveData.LoadTrades läser in datan.
  public Trade(string? senderEmail, string? receiverEmail, Item item, Trade_Status status)
  {
    SenderEmail = senderEmail;
    ReceiverEmail = receiverEmail;
    TradeItemName = item;
    Status = status;
  }

  // Enkla get-metoder för att läsa ut fältvärden.
  // Vi exponerar inte fälten direkt för att kunna ändra implementationen senare utan att bryta annan kod.
  public string? GetSenderEmail()
  {
    return SenderEmail;
  }

  public string? GetReceiverEmail()
  {
    return ReceiverEmail;
  }

  // Namnet GetItemName behålls för kompatibilitet, även om det returnerar hela Item-objektet.
  // (Klassisk "don’t break the API"-regel.)
  public Item GetItemName()
  {
    return TradeItemName;
  }

  public Trade_Status GetStatus()
  {
    return Status;
  }

  // Dessa metoder ändrar statusen för en trade.
  // Vi använder metoder i stället för att sätta fältet direkt för att kunna utöka logiken senare.
  public void Accept()
  {
    Status = Trade_Status.Accepted;
  }

  public void Deny()
  {
    Status = Trade_Status.Denied;
  }
}

// No pirate code hiding here — everything straight, clean and righteous, not a single bombaclat in sight.

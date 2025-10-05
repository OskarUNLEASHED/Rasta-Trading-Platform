namespace App; // Håller ihop klassen med resten av programmet via namespace App

// Klassen Item representerar ett objekt som användare kan lägga upp för byte.
// Den innehåller namn, beskrivning och ägarens e-postadress.
public class Item
{
  // Fälten är markerade som nullable (string?) eftersom filinläsning kan returnera null.
  // Vi skyddar fälten med private så de bara kan nås via metoder nedan.
  private string? ItemName;        // Namn som visas i listor - det användaren ser.
  private string? ItemDescription; // Kort beskrivning – vad handlar prylen om?
  private string? OwnerEmail;      // Ägarens e-postadress - används vid bytesförfrågningar.

  // Konstruktor – sätter värden vid skapandet av ett nytt objekt.
  // Vi skickar in värden direkt här istället för att skapa tomma objekt,
  // för att säkerställa att varje item alltid har ett tydligt startläge.
  public Item(string? itemname, string? itemdescription, string? owneremail)
  {
    ItemName = itemname;            // sätter namn
    ItemDescription = itemdescription; // sätter beskrivning
    OwnerEmail = owneremail;        // sätter ägarens kontakt
  }

  // Metod för att hämta namnet.
  // Att använda en metod istället för att exponera fältet direkt ger kontroll –
  // vi kan senare lägga till validering eller formattering utan att ändra övrig kod.
  public string? GetItemName()
  {
    return ItemName; // ya rasta user, name check, seen?
  }

  // Metod för att hämta beskrivningen – används när man visar listor i UI:t.
  public string? GetItemDescription()
  {
    return ItemDescription; // tell dem what di item seh
  }

  // Metod för att hämta ägarens e-postadress – behövs när man skickar bytesförfrågan.
  public string? GetOwnerEmail()
  {
    return OwnerEmail; // owner link, irie
  }
}

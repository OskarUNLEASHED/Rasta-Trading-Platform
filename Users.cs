namespace App; // Håller ihop klassen med resten av projektet

// Klassen User hanterar inloggning och autentisering.
// Håller e-post och lösenord, enkelt och tydligt.
class User
{
  // Fälten är privata för att skydda användardata.
  // Markerade som nullable (string?) eftersom inläsning från fil kan ge null.
  private string? UserEmail;   // användarens e-postadress
  private string? _password;   // användarens lösenord

  // Konstruktor – sätter värden vid skapande.
  public User(string? useremail, string? password)
  {
    UserEmail = useremail;
    _password = password;
  }

  // Hämtar e-post (används för att koppla till item och trades).
  public string? GetUserEmail()
  {
    return UserEmail;
  }

  // Hämtar lösenord (används endast vid sparning och inloggning).
  public string? GetPassword()
  {
    return _password;
  }

  // Jämför inmatade uppgifter med sparade värden.
  // Returnerar true om det matchar – annars false.
  public bool TryLogin(string? useremail, string? password)
  {
    return useremail == UserEmail && password == _password; // easy check, steady code
  }
}

namespace Application.Utilities;

using BCrypt.Net;

public static class HashPassword
{
  public static string Hash(string text)
  {
    return BCrypt.HashPassword(text);
  }

  public static bool Verify(string text, string hash)
  {
    return BCrypt.Verify(text, hash);
  }
}
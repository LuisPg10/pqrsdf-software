using System.Reflection;

namespace Application.Services;

public class ApplicationAssemblyReference
{
  internal static readonly Assembly ApplicationAssembly = typeof(ApplicationAssemblyReference).Assembly;
}
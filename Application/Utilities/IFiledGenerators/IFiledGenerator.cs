namespace Application.Utilities.IFiledGenerators
{
  public interface IFiledGenerator
  {
    Task<string> GenerateFiledAsync(CancellationToken cancellationToken = default);
  }
}
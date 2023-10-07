namespace AlerterTranslator;

public interface ITranslationService
{
    Task<IEnumerable<string>> Translate(IEnumerable<string> input);
}

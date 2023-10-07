
using System.Text;
using Newtonsoft.Json;

namespace AlerterTranslator;

public class TranslationService : ITranslationService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _region;

    public TranslationService(HttpClient httpClient, string apiKey, string region)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
        _region = region;
    }

    public async Task<IEnumerable<string>> Translate(IEnumerable<string> input)
    {
        var requestBody = input.Select(x => new TranslateText { Text = x }).ToList();
        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&to=en");

        request.Headers.Add("Ocp-Apim-Subscription-Key", _apiKey);
        request.Headers.Add("Ocp-Apim-Subscription-Region", _region);
        request.Content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(request);
        var responseJson = await response.Content.ReadAsStringAsync();
        var responseObj = JsonConvert.DeserializeObject<List<TranslationData>>(responseJson);
        return responseObj!.Select(x => x.Translations.First().Text);
    }
}

public record TranslateText
{
    [JsonProperty("text")]
    public string Text { get; init; }

}


public record DetectedLanguage
{
    [JsonProperty("language")]
    public string Language { get; init; }

    [JsonProperty("score")]
    public double Score { get; init; }
}

public record Translation
{
    [JsonProperty("text")]
    public string Text { get; init; }

    [JsonProperty("to")]
    public string To { get; init; }
}

public record TranslationData
{
    [JsonProperty("detectedLanguage")]
    public DetectedLanguage DetectedLanguage { get; init; }

    [JsonProperty("translations")]
    public List<Translation> Translations { get; init; }
}
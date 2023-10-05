
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace AlerterScraper.Drinnies;

public class DrinniesProductScraper : BaseScraper, IDrinniesProductScraper
{
    private const string _productUrl = "https://api.loveyourartist.com/v1/store/products?filter[profiles][$eq]=63691ad05d62f1822d66e670";
    public DrinniesProductScraper(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        var resp = await _httpClient.GetAsync(_productUrl);
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new ZeroOrTrueConverter());
        var json = await resp.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<Product>>(json, settings);
    }
}

public class ZeroOrTrueConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(bool);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Integer)
        {
            // Check if the integer value is 0, and return false if so.
            if ((Int64)reader.Value == 0 && reader.Path.Contains(".inStock"))
            {
                return false;
            }
        }
        else if (reader.TokenType == JsonToken.Boolean)
        {
            // If it's already a boolean value, return it as is.
            return (bool)reader.Value;
        }

        // If the value is neither 0 nor a boolean, throw an exception or handle it as needed.
        throw new JsonSerializationException("Unexpected value for boolean.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}

namespace AlerterScraper.Drinnies;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public record Price
{
    [JsonProperty("total")]
    public double Total { get; init; }
}

public record Variant
{
    [JsonProperty("price")]
    public Price Price { get; init; } = new();

    [JsonProperty("description")]
    public string Description { get; init; } = string.Empty;

    [JsonProperty("status")]
    public string Status { get; init; } = string.Empty;

    [JsonProperty("soldOut")]
    public bool? SoldOut { get; init; }

    [JsonProperty("_id")]
    public string Id { get; init; } = string.Empty;

    [JsonProperty("insuredShippingOnly")]
    public bool InsuredShippingOnly { get; init; }

    [JsonProperty("inStock")]
    public object InStock { get; init; } = new();

    [JsonProperty("id")]
    public string VariantId { get; init; } = string.Empty;
}

public record Product
{
    [JsonProperty("_id")]
    public string Id { get; init; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; init; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; init; }

    [JsonProperty("shippable")]
    public bool Shippable { get; init; }

    [JsonProperty("variants")]
    public List<Variant> Variants { get; init; } = new();

    [JsonProperty("soldOut")]
    public bool? SoldOut { get; init; }

    [JsonProperty("id")]
    public string ProductId { get; init; } = string.Empty;
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HousingRepairsOnlineApi.Dtos;

public record BuildingsRegisterAddress
{
    [JsonProperty("id")] public string Id { get; init; }

    [JsonProperty("line_1")] public string Line1 { get; init; }

    [JsonProperty("line_2")] public string Line2 { get; init; }

    [JsonProperty("postcode")] public string Postcode { get; init; }

    [JsonProperty("town_or_city")] public string TownOrCity { get; init; }

    [JsonProperty("uprn_list")] public List<Uprn> Uprns { get; init; }
}

public record Uprn
{
    public string Type { get; init; }
    public string Value { get; init; }
}

// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class AddressInbound
{
    [JsonProperty("building_name")]
    [JsonPropertyName("building_name")]
    public string BuildingName { get; set; }

    [JsonProperty("building_number")]
    [JsonPropertyName("building_number")]
    public string BuildingNumber { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string County { get; set; }

    public string Line1 { get; set; }

    public string PostCode { get; set; }

    [JsonProperty("street_name")]
    [JsonPropertyName("street_name")]
    public string StreetName { get; set; }

    [JsonProperty("sub_building_name")]
    [JsonPropertyName("sub_building_name")]
    public string SubBuildingName { get; set; }
}

// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class TransportationInbound
{
    public string BillNumber { get; set; }

    public string ContainerId { get; set; }

    public string ExportDate { get; set; }

    public string ExportLocation { get; set; }

    public string Flag { get; set; }

    public string FlightNumber { get; set; }

    public bool? HasRoadTransportDocument { get; set; }

    [JsonProperty("modeofTransport")]
    [JsonPropertyName("modeofTransport")]
    public ModeOfTransportInbound? ModeOfTransport { get; set; }

    public string Name { get; set; }

    public string Nationality { get; set; }

    public string Registration { get; set; }
}

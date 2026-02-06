// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Defra.Trade.Events.Services.CatchCertificates.Logic.Dto;
using Newtonsoft.Json;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class StorageDocumentCreateInbound : IMmoEntity<V3.Dto.Inbound.ExporterInbound>
{
    public V3.Dto.Inbound.AuthorityInbound Authority { get; set; }

    public string CaseType1 { get; set; }

    public string CaseType2 { get; set; }

    public string CompanyName { get; set; }

    [JsonProperty("_correlationId")]
    [JsonPropertyName("_correlationId")]
    public Guid? CorrelationId { get; set; }

    public string DA { get; set; }

    public DateTime? DocumentDate { get; set; }

    public string DocumentNumber { get; set; }

    public string DocumentUrl { get; set; }

    public V3.Dto.Inbound.CountryInbound ExportedTo { get; set; }

    public V3.Dto.Inbound.ExporterInbound Exporter { get; set; }

    public string ExporterId { get; set; }

    public int? NumberOfFailedSubmissions { get; set; }

    public IEnumerable<V3.Dto.Inbound.ProductInbound> Products { get; set; }

    public bool? RequestedByAdmin { get; set; }

    public V3.Dto.Inbound.StorageFacilityInbound StorageFacility { get; set; }

    public V3.Dto.Inbound.TransportationInbound Transportation { get; set; }

    public int? Version { get; set; }
}

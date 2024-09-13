// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Defra.Trade.Events.Services.CatchCertificates.Logic.Dto;
using Newtonsoft.Json;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class ProcessingStatementCreateInbound : IMmoEntity<ExporterInbound>
{
    public AuthorityInbound Authority { get; set; }

    public string CaseType1 { get; set; }

    public string CaseType2 { get; set; }

    public IEnumerable<CatchInbound> Catches { get; set; }

    [JsonProperty("_correlationId")]
    [JsonPropertyName("_correlationId")]
    public Guid? CorrelationId { get; set; }

    public string DA { get; set; }

    public DateTime? DocumentDate { get; set; }

    public string DocumentNumber { get; set; }

    public string DocumentUrl { get; set; }

    public CountryInbound ExportedTo { get; set; }

    public ExporterInbound Exporter { get; set; }

    public string ExporterId { get; set; }

    public string HealthCertificateDate { get; set; }

    public string HealthCertificateNumber { get; set; }

    public int? NumberOfFailedSubmissions { get; set; }

    public string PersonResponsible { get; set; }

    public AddressInbound PlantAddress { get; set; }

    public string PlantApprovalNumber { get; set; }

    public string PlantDateOfAcceptance { get; set; }

    public string PlantName { get; set; }

    public string ProcessedFisheryProducts { get; set; }

    public bool? RequestedByAdmin { get; set; }

    public int? Version { get; set; }
}

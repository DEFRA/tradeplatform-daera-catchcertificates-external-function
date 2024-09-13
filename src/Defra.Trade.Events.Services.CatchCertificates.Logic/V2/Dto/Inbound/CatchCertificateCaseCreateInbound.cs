// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Defra.Trade.Events.Services.CatchCertificates.Logic.Dto;
using Newtonsoft.Json;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class CatchCertificateCaseCreateInbound : IMmoEntity<ExporterInbound>
{
    public IEnumerable<AuditInbound> Audits { get; set; }

    public string CaseType1 { get; set; }

    public string CaseType2 { get; set; }

    public string CertStatus { get; set; }

    [JsonProperty("_correlationId")]
    [JsonPropertyName("_correlationId")]
    public Guid? CorrelationId { get; set; }

    public string DA { get; set; }

    public DateTime? DocumentDate { get; set; }

    public string DocumentNumber { get; set; }

    public string DocumentUrl { get; set; }

    public CountryInbound ExportedTo { get; set; }

    public ExporterInbound Exporter { get; set; }

    public bool? FailureIrrespectiveOfRisk { get; set; }

    public bool? IsDirectLanding { get; set; }

    public bool? IsUnblocked { get; set; }

    public IEnumerable<LandingInbound> Landings { get; set; }

    public bool? MultiVesselSchedule { get; set; }

    public int? NumberOfFailedSubmissions { get; set; }

    public bool? RequestedByAdmin { get; set; }

    public bool? SpeciesOverriddenByAdmin { get; set; }

    public TransportationInbound Transportation { get; set; }

    public int? Version { get; set; }

    public bool? VesselOverriddenByAdmin { get; set; }
}

// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

public class LandingInbound
{
    public string CnCode { get; set; }

    public string CommodityCodeDescription { get; set; }

    public string Id { get; set; }

    public bool Is14DayLimitReached { get; set; }

    public DateTime? LandingDate { get; set; }

    public string LicenceHolder { get; set; }

    public int NumberOfTotalSubmissions { get; set; }

    public string Presentation { get; set; }

    public RiskingInbound Risking { get; set; }

    public string ScientificName { get; set; }

    public string Source { get; set; }

    public string Species { get; set; }

    public string SpeciesAlias { get; set; }

    public string State { get; set; }

    public string Status { get; set; }

    public LandingValidationInbound Validation { get; set; }

    public double? VesselLength { get; set; }

    public string VesselName { get; set; }

    public bool? VesselOverriddenByAdmin { get; set; }

    public string VesselPln { get; set; }

    public bool? WasValidationDueAtPointOfApplication { get; set; }

    public double Weight { get; set; }
}

// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

public class RiskingInbound
{
    public string ExporterRiskScore { get; set; }

    public string HighOrLowRisk { get; set; }

    public bool IsSpeciesRiskEnabled { get; set; }

    public string LandingRiskScore { get; set; }

    public IEnumerable<string> OveruseInfo { get; set; }

    public string SpeciesRisk { get; set; }

    public string Vessel { get; set; }
}

// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

public class LandingValidationInbound
{
    public bool IsLegallyDue { get; set; }

    public double? LandedWeightExceededBy { get; set; }

    public double LiveExportWeight { get; set; }

    public Uri RawLandingsUrl { get; set; }

    public Uri SalesNoteUrl { get; set; }

    public double? TotalEstimatedForExportSpecies { get; set; }

    public double? TotalEstimatedWithTolerance { get; set; }

    public double? TotalLiveForExportSpecies { get; set; }

    public double? TotalRecordedAgainstLanding { get; set; }

    public double? TotalWeightForSpecies { get; set; }
}

// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class CatchValidationInbound
{
    public List<string> OveruseInfo { get; set; }

    public string Status { get; set; }

    public double? TotalUsedWeightAgainstCertificate { get; set; }

    public double? WeightExceededAmount { get; set; }
}

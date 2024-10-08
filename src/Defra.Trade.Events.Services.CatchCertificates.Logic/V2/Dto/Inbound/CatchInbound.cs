﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class CatchInbound
{
    public string CnCode { get; set; }

    public string ForeignCatchCertificateNumber { get; set; }

    public string Id { get; set; }

    public double? ImportedWeight { get; set; }

    public double? ProcessedWeight { get; set; }

    public string ScientificName { get; set; }

    public string Species { get; set; }

    public double? UsedWeightAgainstCertificate { get; set; }

    public CatchValidationInbound Validation { get; set; }
}

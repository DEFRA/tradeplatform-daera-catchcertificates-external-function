// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

public class CountryInbound
{
    public string IsoCodeAlpha2 { get; set; }

    public string IsoCodeAlpha3 { get; set; }

    public string IsoNumericCode { get; set; }

    public string OfficialCountryName { get; set; }
}

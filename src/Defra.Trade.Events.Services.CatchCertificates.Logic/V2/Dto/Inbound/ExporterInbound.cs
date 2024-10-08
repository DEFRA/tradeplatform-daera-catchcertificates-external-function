// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class ExporterInbound
{
    public string AccountId { get; set; }

    public AddressInbound Address { get; set; }

    public string CompanyName { get; set; }

    public string ContactId { get; set; }

    public DynamicsAddressInbound DynamicsAddress { get; set; }

    public string FullName { get; set; }
}

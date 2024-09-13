// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class AuthorityInbound
{
    public AddressInbound Address { get; set; }

    public string CompanyName { get; set; }

    public string DateIssued { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Tel { get; set; }
}

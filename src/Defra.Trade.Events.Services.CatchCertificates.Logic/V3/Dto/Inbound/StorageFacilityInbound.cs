// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Dto.Inbound;

public class StorageFacilityInbound
{
    public AddressInbound Address { get; set; }

    public string ApprovalNumber { get; set; }

    public string DateOfUnloading { get; set; }

    public string Name { get; set; }

    public string ProductHandling { get; set; }
}

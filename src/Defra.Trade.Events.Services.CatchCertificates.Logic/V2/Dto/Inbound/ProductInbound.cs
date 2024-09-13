// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class ProductInbound
{
    public string CnCode { get; set; }

    public string DateOfUnloading { get; set; }

    public double? ExportedWeight { get; set; }

    public string ForeignCatchCertificateNumber { get; set; }

    public string Id { get; set; }

    public double? ImportedWeight { get; set; }

    public string PlaceOfUnloading { get; set; }

    public string ScientificName { get; set; }

    public string Species { get; set; }

    public string TransportUnloadedFrom { get; set; }

    public ProductValidationInbound Validation { get; set; }
}

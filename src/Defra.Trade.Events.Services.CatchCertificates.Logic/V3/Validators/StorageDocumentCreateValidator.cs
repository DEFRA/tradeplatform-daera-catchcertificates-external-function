// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.Services.CatchCertificates.Logic.Validators;
using FluentValidation;
using V3Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Validators;

public class StorageDocumentCreateValidator : AbstractValidator<V3Inbound.StorageDocumentCreateInbound>
{
    public StorageDocumentCreateValidator()
    {
        this.AddMmoEntityValidationRules<V3Inbound.StorageDocumentCreateInbound, V3Inbound.ExporterInbound>();

        RuleFor(x => x.CompanyName).NotNull().NotEmpty();
    }
}

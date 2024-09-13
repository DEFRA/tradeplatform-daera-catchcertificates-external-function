// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.Services.CatchCertificates.Logic.Validators;
using FluentValidation;
using V1Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Validators;

public class StorageDocumentCreateValidator : AbstractValidator<V1Inbound.StorageDocumentCreateInbound>
{
    public StorageDocumentCreateValidator()
    {
        this.AddMmoEntityValidationRules<V1Inbound.StorageDocumentCreateInbound, V1Inbound.ExporterInbound>();

        RuleFor(x => x.CompanyName).NotNull().NotEmpty();
    }
}

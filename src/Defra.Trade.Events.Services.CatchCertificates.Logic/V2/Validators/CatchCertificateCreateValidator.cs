// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.Services.CatchCertificates.Logic.Validators;
using FluentValidation;
using V2Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Validators;

public class CatchCertificateCreateValidator : AbstractValidator<V2Inbound.CatchCertificateCaseCreateInbound>
{
    public CatchCertificateCreateValidator()
    {
        this.AddMmoEntityValidationRules<V2Inbound.CatchCertificateCaseCreateInbound, V2Inbound.ExporterInbound>();
    }
}

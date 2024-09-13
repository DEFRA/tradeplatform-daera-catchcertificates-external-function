// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Dto;

public interface IMmoEntity<out TExporter>
{
    string CaseType1 { get; }

    string CaseType2 { get; }

    Guid? CorrelationId { get; }

    string DocumentNumber { get; }

    TExporter Exporter { get; }

    int? NumberOfFailedSubmissions { get; }

    bool? RequestedByAdmin { get; }
}

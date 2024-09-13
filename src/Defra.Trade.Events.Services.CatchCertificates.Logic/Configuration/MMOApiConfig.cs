// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Configuration;

public class MmoApiConfig
{
    public const string AppSettingsName = nameof(MmoApiConfig);

    public string BaseAddress { get; set; }
}

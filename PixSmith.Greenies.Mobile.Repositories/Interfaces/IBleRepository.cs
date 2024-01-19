﻿using PixSmith.Greenies.Mobile.Domain.Models;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;

namespace PixSmith.Greenies.Mobile.Repositories.Interfaces;

public interface IBleRepository
{
    Task StartScanningFiltered(EventHandler<DeviceEventArgs> deviceDiscoveredEvent,
        ScanFilterOptions scanFilterOptions,
        BleScanningParameterModel bleScanningParameterModel);

    Task StartScanning(EventHandler<DeviceEventArgs> deviceDiscoveredEvent,
        BleScanningParameterModel bleScanningParameterModel);

    Task<bool> ConnectToDeviceAsync(IDevice device, CancellationToken ct);

    Task<IService[]> ListDeviceServicesAsync(IDevice device, CancellationToken ct);
}
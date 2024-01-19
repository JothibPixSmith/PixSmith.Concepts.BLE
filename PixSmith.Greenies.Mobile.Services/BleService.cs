using PixSmith.Greenies.Mobile.Domain.Models;
using PixSmith.Greenies.Mobile.Repositories.Interfaces;
using PixSmith.Greenies.Mobile.Services.Interfaces;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;

namespace PixSmith.Greenies.Mobile.Services
{
    public class BleService : IBleService
    {
        private readonly IBleRepository bleRepository;

        public BleService(IBleRepository bleRepository)
        {
            this.bleRepository = bleRepository;
        }

        public async Task StartScanningFiltered(EventHandler<DeviceEventArgs> deviceDiscoveredEvent,
            ScanFilterOptions scanFilterOptions,
            BleScanningParameterModel bleScanningParameterModel)
        {
            await this.bleRepository.StartScanningFiltered(deviceDiscoveredEvent, scanFilterOptions,
                bleScanningParameterModel);
        }

        public async Task StartScanning(EventHandler<DeviceEventArgs> deviceDiscoveredEvent,
            BleScanningParameterModel bleScanningParameterModel)
        {
            await this.bleRepository.StartScanning(deviceDiscoveredEvent,
                bleScanningParameterModel);
        }

        public async Task<bool> ConnectToDeviceAsync(IDevice device, CancellationToken ct)
        {
            return await this.bleRepository.ConnectToDeviceAsync(device, ct);
        }

        public async Task<IService[]> ListDeviceServices(IDevice device, CancellationToken ct)
        {
            if (!await this.bleRepository.ConnectToDeviceAsync(device, ct))
            {
                return Array.Empty<IService>();
            }

            return await this.bleRepository.ListDeviceServicesAsync(device, ct);
        }
    }
}
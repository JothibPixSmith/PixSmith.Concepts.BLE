using PixSmith.Greenies.Mobile.Domain.Models;
using PixSmith.Greenies.Mobile.Repositories.Interfaces;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;

namespace PixSmith.Greenies.Mobile.Repositories
{
    public class BleRepository : IBleRepository
    {
        private readonly IBluetoothLE bluetoothLe;

        public BleRepository(IBluetoothLE bluetoothLe)
        {
            this.bluetoothLe = bluetoothLe;
        }

        public async Task StartScanning(EventHandler<DeviceEventArgs> deviceDiscoveredEvent,
            BleScanningParameterModel bleScanningParameterModel)
        {
            this.SetBleScanningParameters(bleScanningParameterModel);

            this.bluetoothLe.Adapter.DeviceDiscovered += deviceDiscoveredEvent;

            await this.bluetoothLe.Adapter.StartScanningForDevicesAsync();
        }

        public async Task<bool> ConnectToDeviceAsync(IDevice device, CancellationToken ct)
        {
            try
            {
                await this.bluetoothLe.Adapter.ConnectToDeviceAsync(device, cancellationToken: ct);
            }
            catch (DeviceConnectionException e)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<IService[]> ListDeviceServicesAsync(IDevice device, CancellationToken ct)
        {
            return (await device.GetServicesAsync(ct))?.ToArray() ?? Array.Empty<IService>();
        }

        public async Task StartScanningFiltered(EventHandler<DeviceEventArgs> deviceDiscoveredEvent,
        ScanFilterOptions scanFilterOptions,
        BleScanningParameterModel bleScanningParameterModel)
        {
            this.SetBleScanningParameters(bleScanningParameterModel);

            this.bluetoothLe.Adapter.DeviceDiscovered += deviceDiscoveredEvent;

            await this.bluetoothLe.Adapter.StartScanningForDevicesAsync(scanFilterOptions);
        }

        private void SetBleScanningParameters(BleScanningParameterModel bleScanningParameterModel)
        {
            this.bluetoothLe.Adapter.ScanMode = bleScanningParameterModel.ScanMode;

            this.bluetoothLe.Adapter.ScanTimeout = bleScanningParameterModel.ScanTimeout;
        }



    }
}
using CommunityToolkit.Mvvm.Input;
using PixSmith.Greenies.Mobile.Domain.Models;
using PixSmith.Greenies.Mobile.Services.Interfaces;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace PixSmith.Greenies.Mobile.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly IBleService bleService;

    public MainPageViewModel(IBleService bleService)
    {
        this.bleService = bleService;

        this.OnScanClick = new AsyncRelayCommand(this.StartScan);

        this.OnDeviceClick = new AsyncRelayCommand(this.StartListingServices);

        this.OnServiceClick = new AsyncRelayCommand(this.StartListingCharacteristics);

        this.OnCharacteristicClick = new AsyncRelayCommand(this.StartGettingCharacteristicValue);

        this.Devices = new ObservableCollection<IDevice>();
    }

    public ICommand OnScanClick { get; set; }

    public ICommand OnDeviceClick { get; set; }

    public ICommand OnServiceClick { get; set; }

    public ICommand OnCharacteristicClick { get; set; }

    public ObservableCollection<IDevice> Devices { get; private set; }

    public ObservableCollection<IService> DeviceServices { get; private set; }

    public ObservableCollection<ICharacteristic> ServiceCharacteristics { get; private set; }

    public IDevice SelectedDevice { get; set; }

    public IService SelectedService { get; set; }

    public ICharacteristic SelectedCharacteristic { get; set; }

    public string GattCharacteristic { get; set; }

    public async Task StartScan()
    {
        this.Devices.Clear();

        await this.bleService.StartScanning(this.DeviceDiscovered, new BleScanningParameterModel
        {
            ScanMode = ScanMode.Balanced,
            ScanTimeout = 10000,
        });
    }

    public async Task StartListingServices(CancellationToken ct)
    {
        this.DeviceServices?.Clear();
        this.DeviceServices = new ObservableCollection<IService>(await this.bleService.ListDeviceServices(this.SelectedDevice, ct));
        this.OnPropertyChanged(nameof(DeviceServices));
    }

    public async Task StartListingCharacteristics(CancellationToken ct)
    {
        try
        {
            this.ServiceCharacteristics?.Clear();
            this.ServiceCharacteristics = new ObservableCollection<ICharacteristic>(await this.SelectedService.GetCharacteristicsAsync());
            this.OnPropertyChanged(nameof(ServiceCharacteristics));
        }
        catch(Exception ex)
        {

        }
    }

    public async Task StartGettingCharacteristicValue(CancellationToken ct)
    {
        var data = await this.SelectedCharacteristic.ReadAsync(ct);
        this.GattCharacteristic = BitConverter.ToSingle(data.data).ToString();
        this.OnPropertyChanged(nameof(GattCharacteristic));
        Thread.Sleep(500);
        await this.StartGettingCharacteristicValue(ct);
    }

    private void SelectedCharacteristic_ValueUpdated(object sender, CharacteristicUpdatedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DeviceDiscovered(object sender, DeviceEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (!Devices.Contains(e.Device))
            {
                this.Devices.Add(e.Device);
            }
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }
}
using Plugin.BLE.Abstractions.Contracts;

namespace PixSmith.Greenies.Mobile.Domain.Models
{
    public class BleScanningParameterModel
    {
        public int ScanTimeout { get; set; }

        public ScanMode ScanMode { get; set; }
    }
}

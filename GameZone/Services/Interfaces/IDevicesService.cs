namespace GameZone.Services.Interfaces;

public interface IDevicesService
{
    IEnumerable<SelectListItem> GetSelectListDevices();
}
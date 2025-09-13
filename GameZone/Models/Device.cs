namespace GameZone.Models;

public class Device : BaseEntity
{
    [MaxLength(50)]
    public string Icon { get; set; } = default!;
    public ICollection<GameDevice> GameDevices { get; set; } = default!;
}
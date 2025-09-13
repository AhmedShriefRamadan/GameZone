namespace GameZone.Models;

public class Game : BaseEntity
{
    [MaxLength(2555)]
    public string Description { get; set; } = default!;
    [MaxLength(500)]
    public string Cover { get; set; } = default!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public ICollection<GameDevice> GameDevices { get; set; } = default!;
}
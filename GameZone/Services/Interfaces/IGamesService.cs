using GameZone.ViewModels;

namespace GameZone.Services.Interfaces;

public interface IGamesService
{
    public Task Create(CreateGameFormViewModel model);
}
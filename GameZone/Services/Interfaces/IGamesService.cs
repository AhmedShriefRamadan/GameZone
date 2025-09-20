using GameZone.ViewModels;

namespace GameZone.Services.Interfaces;

public interface IGamesService
{
    public Task Create(CreateGameFormViewModel model);
    public Task<Game?> GetById(int id);
    public Task<IEnumerable<Game>> GetAll();
    public Task<Game?> Update(UpdateGameFormViewModel model);
}
namespace GameZone.Services.Interfaces;

public interface ICategoriesService
{
    public IEnumerable<SelectListItem> GetSelectListCategories();
}
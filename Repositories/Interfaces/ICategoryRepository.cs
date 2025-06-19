using BusinessObjects;

namespace Repositories.Interfaces
{
	public interface ICategoryRepository
	{
		List<Category> GetCategories();
		Category GetCategoryById(int id);
		void SaveCategory(Category category);
		void UpdateCategory(Category category);
		void DeleteCategory(int id);
	}
}

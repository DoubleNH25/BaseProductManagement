using BusinessObjects;

namespace Services.Interfaces
{
	public interface ICategoryService
	{
		List<Category> GetCategories();
		Category GetCategoryById(int id);
		void SaveCategory(Category category);
		void UpdateCategory(Category category);
		void DeleteCategory(int id);
	}
}

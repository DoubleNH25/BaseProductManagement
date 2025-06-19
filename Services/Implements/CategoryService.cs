using BusinessObjects;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public List<Category> GetCategories()
		{
			return _categoryRepository.GetCategories();
		}

		public Category GetCategoryById(int id)
		{
			return _categoryRepository.GetCategoryById(id);
		}

		public void SaveCategory(Category category)
		{
			_categoryRepository.SaveCategory(category);
		}

		public void UpdateCategory(Category category)
		{
			_categoryRepository.UpdateCategory(category);
		}

		public void DeleteCategory(int id)
		{
			_categoryRepository.DeleteCategory(id);
		}
	}
}

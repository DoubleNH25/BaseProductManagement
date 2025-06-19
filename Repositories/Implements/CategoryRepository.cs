using BusinessObjects;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories.Implements
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly CategoryDAO _categoryDAO;

		public CategoryRepository(MyStoreContext context)
		{
			_categoryDAO = new CategoryDAO(context);
		}

		public List<Category> GetCategories()
		{
			return _categoryDAO.GetCategories();
		}

		public Category GetCategoryById(int id)
		{
			return _categoryDAO.GetCategoryById(id);
		}

		public void SaveCategory(Category category)
		{
			_categoryDAO.SaveCategory(category);
		}

		public void UpdateCategory(Category category)
		{
			_categoryDAO.UpdateCategory(category);
		}

		public void DeleteCategory(int id)
		{
			_categoryDAO.DeleteCategory(id);
		}
	}
}

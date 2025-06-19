using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
	public class CategoryDAO
	{
		private readonly MyStoreContext _context;

		public CategoryDAO(MyStoreContext context)
		{
			_context = context;
		}

		public List<Category> GetCategories()
		{
			return _context.Categories.ToList();
		}

		public Category? GetCategoryById(int id)
		{
			return _context.Categories.Find(id);
		}

		public void SaveCategory(Category category)
		{
			_context.Categories.Add(category);
			_context.SaveChanges();
		}

		public void UpdateCategory(Category category)
		{
			_context.Entry(category).State = EntityState.Modified;
			_context.SaveChanges();
		}

		public void DeleteCategory(int id)
		{
			var category = _context.Categories.Find(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
				_context.SaveChanges();
			}
		}
	}
}

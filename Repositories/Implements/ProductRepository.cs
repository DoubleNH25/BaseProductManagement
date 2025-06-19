

using BusinessObjects;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories.Implements
{
	public class ProductRepository : IProductRepository
	{
		private readonly ProductDAO _productDAO;

		public ProductRepository(MyStoreContext context)
		{
			_productDAO = new ProductDAO(context);
		}

		public List<Product> GetProducts()
		{
			return _productDAO.GetProducts();
		}

		public Product GetProductById(int id)
		{
			return _productDAO.GetProductById(id);
		}

		public void SaveProduct(Product product)
		{
			_productDAO.SaveProduct(product);
		}

		public void UpdateProduct(Product product)
		{
			_productDAO.UpdateProduct(product);
		}

		public void DeleteProduct(Product product)
		{
			_productDAO.DeleteProduct(product);
		}
	}
}

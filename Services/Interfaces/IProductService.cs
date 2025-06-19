

using BusinessObjects;

namespace Services.Interfaces
{
	public interface IProductService
	{
		List<Product> GetProducts();
		Product GetProductById(int id);
		void SaveProduct(Product product);
		void UpdateProduct(Product product);
		void DeleteProduct(Product product);
	}
}

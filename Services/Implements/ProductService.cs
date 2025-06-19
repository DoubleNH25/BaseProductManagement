﻿

using BusinessObjects;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public List<Product> GetProducts()
		{
			return _productRepository.GetProducts();
		}

		public Product GetProductById(int id)
		{
			return _productRepository.GetProductById(id);
		}

		public void SaveProduct(Product product)
		{
			_productRepository.SaveProduct(product);
		}

		public void UpdateProduct(Product product)
		{
			_productRepository.UpdateProduct(product);
		}

		public void DeleteProduct(Product product)
		{
			_productRepository.DeleteProduct(product);
		}
	}
}

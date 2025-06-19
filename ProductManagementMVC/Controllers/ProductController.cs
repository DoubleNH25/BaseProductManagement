using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ProductManagementMVC.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // Hiển thị danh sách sản phẩm - Admin có thể CRUD, User chỉ xem
        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var products = _productService.GetProducts();
            return View(products);
        }

        // Chỉ Admin mới được tạo sản phẩm
        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            ViewBag.Categories = _categoryService.GetCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            if (ModelState.IsValid)
            {
                _productService.SaveProduct(product);
                TempData["Success"] = "Tạo sản phẩm thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetCategories();
            return View(product);
        }

        // Chỉ Admin mới được xem chi tiết và edit
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _categoryService.GetCategories();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                TempData["Success"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetCategories();
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var product = _productService.GetProductById(id);
            if (product != null)
            {
                _productService.DeleteProduct(product);
                TempData["Success"] = "Xóa sản phẩm thành công!";
            }

            return RedirectToAction("Index");
        }
    }
} 
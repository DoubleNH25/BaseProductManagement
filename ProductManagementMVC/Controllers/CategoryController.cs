using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ProductManagementMVC.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Chỉ Admin mới được quản lý category
        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var categories = _categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            if (ModelState.IsValid)
            {
                _categoryService.SaveCategory(category);
                TempData["Success"] = "Tạo danh mục thành công!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                TempData["Success"] = "Cập nhật danh mục thành công!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            _categoryService.DeleteCategory(id);
            TempData["Success"] = "Xóa danh mục thành công!";
            return RedirectToAction("Index");
        }
    }
} 
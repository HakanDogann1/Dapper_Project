using Dapper;
using Dapper_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Dapper_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string _connection = "Server=DESKTOP-NOMRM5V\\SQLEXPRESS;initial catalog=DbNews;integrated security=true;";
        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connection);
            var values = await connection.QueryAsync<ResultCategoryViewModel>("Select * From Category");
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(ResultCategoryViewModel resultCategory)
        {
            await using var connection = new SqlConnection(_connection);
            var query = $"Insert Into Category (CategoryName , CategoryStatus) Values ('{resultCategory.CategoryName}','True')";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await using var connection = new SqlConnection(_connection);
            var query = $"Delete From Category Where CategoryID='{id}'";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            await using var connection = new SqlConnection(_connection);
            var query = $"Select * From Category Where CategoryID='{id}'";
            var values = await connection.QueryFirstAsync<ResultCategoryViewModel>(query);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(ResultCategoryViewModel model)
        {
            await using var connection = new SqlConnection(_connection);
            var query = $"Update Category SET CategoryName='{model.CategoryName}',CategoryName='{model.CategoryStatus}' Values CategoryID='{model.CategoryID}')";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }
    }
}

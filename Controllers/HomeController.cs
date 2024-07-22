using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendSmartDbContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expense()
        {
            var allExpenses = _context.ExpenseViews.ToList();
            var totalExpenses = allExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalExpenses;
            ViewBag.Message = TempData["Message"]?.ToString();
            
            return View(allExpenses);

         
        }

        public IActionResult CreateEditExpense(int? id)
        {
            var currencies = _context.Currencies.ToList();
            ViewBag.Currencies = currencies;

            if (id != null)
            {
                var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
                return View(expenseInDb);
            }
            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
            if (expenseInDb != null)
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_ManageExpense @Operation, @Id",
                    new SqlParameter("@Operation", 'D'),
                    new SqlParameter("@Id", id)
                );
                TempData["Message"] = "Expense deleted successfully!";

            }
            return RedirectToAction("Expense");
        }

        [HttpPost]
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_ManageExpense @Operation, @Id, @Value, @Description, @Curr_ID",
                    new SqlParameter("@Operation", 'I'),
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Value", model.Value),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Curr_ID", model.Curr_ID)
                );
                TempData["Message"] = "Expense added successfully!";
     
            }
            else
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_ManageExpense @Operation, @Id, @Value, @Description, @Curr_ID",
                    new SqlParameter("@Operation", 'U'),
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Value", model.Value),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Curr_ID", model.Curr_ID)
                );
                TempData["Message"] = "Expense updated successfully!";
                TempData["MessageType"] = "success";
            }

            return RedirectToAction("Expense");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

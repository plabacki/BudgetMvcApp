using BudgetMvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetMvcApp.Models;

public class ExpenseController : Controller
{
    private readonly BudgetMvcAppContext _context;

    public ExpenseController(BudgetMvcAppContext context)
    {
        _context = context;
    }

    public async Task<ActionResult> Index()
    {
        var expenses = await _context.Expenses.Include(t => t.Transaction).ToListAsync();
        return View(expenses);
    }
}
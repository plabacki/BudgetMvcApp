using BudgetMvcApp.Controllers;
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

    public async Task<ActionResult> Index(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        
        var expenses = from e in _context.Expenses select e;
        expenses = expenses.Where(e=> e.TransactionId == id);
        return View(await expenses.ToListAsync());
    }

    public IActionResult Create()
    {
        ViewBag.Transaction = _context.Transaction.ToList();
        return View();
    }

    [HttpPost]

    public IActionResult Create(Expense expense)
    {
        _context.Expenses.Add(expense);
        _context.SaveChanges();
        return RedirectToAction("Index","TransactionController");
    }
}
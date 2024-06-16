using BudgetMvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetMvcApp.Controllers;

public class TransactionController : Controller
{
    private readonly BudgetMvcAppContext _context;

    public TransactionController(BudgetMvcAppContext context)
    {
        _context = context;
    }

    public async Task<ActionResult> Index()
    {
        var transactions = await _context.Transaction.ToListAsync();
        return View(transactions);
    }
}
using BudgetMvcApp.Data;
using BudgetMvcApp.Models;
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Transaction transaction)
    {
        _context.Transaction.Add(transaction);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Delete(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transaction.FirstOrDefaultAsync(t => t.Id == id);
        if(transaction == null)
        {
            return NotFound();
        }
        return View(transaction);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(int id)
    {
        var transaction = await _context.Transaction.FindAsync(id);
        if(transaction != null)
        {
            _context.Transaction.Remove(transaction);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
using BudgetMvcApp.Controllers;
using BudgetMvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        return View(await expenses.Include(t => t.Transaction).ToListAsync());
    }

    public IActionResult Create()
    {
        ViewBag.Transaction = _context.Transaction.ToList();
        return View();
    }

    [HttpPost]

    public async Task<ActionResult> Create(Expense expense)
    {
        if(ModelState.IsValid)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Transaction");
        }
        return View(expense);
    }   

    public async Task<ActionResult> Edit (int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        ViewBag.Transaction = _context.Transaction.ToList();
        var expense = await _context.Expenses.FindAsync(id);
        if(expense == null)
        {
            return NotFound();
        }
        return View(expense);
    }
    [HttpPost]
    public async Task<ActionResult> Edit (int id, Expense expense)
    {
        if(ModelState.IsValid)
        {
            try
            {
                _context.Expenses.Update(expense);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!_context.Expenses.Any(e => e.Id == expense.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index),nameof(Transaction));
            
        }
        return View(expense);
    }

    public async Task<ActionResult> Delete(int? id)
    {
        if(id == null )
        {
            return NotFound();
        }
        var expense = await _context.Expenses.Include(t=> t.Transaction).FirstOrDefaultAsync(e => e.Id == id);
        if(expense == null)
        {
            return NotFound();
        }
        return View(expense);
    }
    [HttpPost]
    public async Task<ActionResult> Delete(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if(expense != null)
        {
            _context.Expenses.Remove(expense);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index),nameof(Transaction));

    }
}
using System.ComponentModel.DataAnnotations;

namespace BudgetMvcApp.Models;

public class Transaction
{
    public int Id {get;set;}
    [StringLength(30, MinimumLength = 2)]
    [Required]
    public string? Name {get;set;}

    public List<Expense> Expenses {get;set;} = [];
}
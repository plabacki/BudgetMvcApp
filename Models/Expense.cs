using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace BudgetMvcApp.Models;

public class Expense
{
    public int Id {get;set;}
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [StringLength(30, MinimumLength = 2)]
    [Required]
    public string? Name {get;set;}
    [Range(1,500)]
    [DataType(DataType.Currency)]
    public decimal Amount {get;set;}
    [DataType(DataType.Date)]
    public DateTime Date {get;set;}
    public int TransactionId {get;set;}
    public Transaction? Transaction {get;set;}
}
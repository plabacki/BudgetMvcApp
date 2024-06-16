using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace BudgetMvcApp.Models;

public class Expense
{
    public int Id {get;set;}
    public string? Name {get;set;}
    public decimal Amount {get;set;}
    [DataType(DataType.Date)]
    public DateTime Date {get;set;}
    public int TransactionId {get;set;}
    public Transaction Transaction {get;set;}
}
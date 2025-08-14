public class Transaction {
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; } // Deposit or Withdraw
    public decimal Amount { get; set; }
}


namespace DefaultNamespace;

public class Payment
{
    private int PaymentId { get; set; }
    private int BookingId { get; set; }
    private decimal Amount  { get; set; }
    private decimal TotalPrice { get; set; }
    private decimal SalesTax { get; set; }
    private decimal Fees { get; set; }
    private DateTime PaymentDate { get; set; }
    private string TransactionLink { get; set; }
}
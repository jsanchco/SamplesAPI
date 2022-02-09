namespace ConsoleApp.ToteLink.Models
{
    public class RequestPuntoPagoIngresarCredito
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ReferenceId { get; set; }
    }
}

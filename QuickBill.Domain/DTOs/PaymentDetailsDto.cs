namespace QuickBill.Domain.DTOs
{
    public class PaymentDetailsDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string BankName { get; set; } = "";
        public string AccountHolderName { get; set; } = "";
        public string AccountNumber { get; set; } = "";
        public string IFSCCode { get; set; } = "";
        public string? UPIId { get; set; }
        public string? PaymentTerms { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 
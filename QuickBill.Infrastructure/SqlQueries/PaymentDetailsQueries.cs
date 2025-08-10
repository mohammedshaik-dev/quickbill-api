namespace QuickBill.Infrastructure.SqlQueries
{
    public static class PaymentDetailsQueries
    {
        public const string InsertPaymentDetails = @"
INSERT INTO paymentdetails (userid, bankname, accountholdername, accountnumber, ifsccode, upiid, paymentterms)
VALUES (@UserId, @BankName, @AccountHolderName, @AccountNumber, @IFSCCode, @UPIId, @PaymentTerms)
RETURNING id;";

        public const string GetAllPaymentDetailsByUserId = @"
SELECT 
    id AS Id,
    userid AS UserId,
    bankname AS BankName,
    accountholdername AS AccountHolderName,
    accountnumber AS AccountNumber,
    ifsccode AS IFSCCode,
    upiid AS UPIId,
    paymentterms AS PaymentTerms,
    createdat AS CreatedAt,
    updatedat AS UpdatedAt,
    isdeleted AS IsDeleted
FROM paymentdetails
WHERE userid = @UserId AND isdeleted = FALSE
ORDER BY createdat DESC;";

        public const string GetPaymentDetailsById = @"
SELECT 
    id AS Id,
    userid AS UserId,
    bankname AS BankName,
    accountholdername AS AccountHolderName,
    accountnumber AS AccountNumber,
    ifsccode AS IFSCCode,
    upiid AS UPIId,
    paymentterms AS PaymentTerms,
    createdat AS CreatedAt,
    updatedat AS UpdatedAt,
    isdeleted AS IsDeleted
FROM paymentdetails
WHERE id = @Id AND isdeleted = FALSE;";

        public const string UpdatePaymentDetails = @"
UPDATE paymentdetails SET
    bankname = @BankName,
    accountholdername = @AccountHolderName,
    accountnumber = @AccountNumber,
    ifsccode = @IFSCCode,
    upiid = @UPIId,
    paymentterms = @PaymentTerms,
    updatedat = NOW()
WHERE id = @Id AND isdeleted = FALSE;";

        public const string SoftDeletePaymentDetails = @"
UPDATE paymentdetails SET isdeleted = TRUE WHERE id = @Id;";
    }
} 
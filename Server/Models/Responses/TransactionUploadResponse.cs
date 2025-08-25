using System.Collections.Generic;
using MoneyTracker2.Models.EntityModels;

namespace MoneyTracker2.Models.Responses;

public record TransactionUploadResponse(
    List<Transaction> Transactions,
    int DuplicatesCount,
    int FailedCount
);
import type { Transaction } from "../transaction"

export type TransactionUploadResponse = {
    transactions: Transaction[],
    duplicatesCount: number,
}
import type { Category } from "./category"

export type Transaction = {
    id: number,
    date: Date,
    amount: number,
    contact: string,
    reference: string,
    category: Category,
}
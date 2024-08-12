import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia'
import type { Transaction } from '@/types/transaction'

export const useTransactionStore = defineStore('transactions', () => {
  const transactions: Ref<Transaction[]> = ref([])

  function setTransaction(data: Transaction) {
    const transaction = transactions.value.find((t) => t.id === data.id)
    if (transaction) {
      Object.assign(transaction, data)
    } else {
      transactions.value.push(data)
    }
  }

  function setTransactions(data: Transaction[]) {
    transactions.value = data
  }

  return { transactions, setTransaction, setTransactions }
})

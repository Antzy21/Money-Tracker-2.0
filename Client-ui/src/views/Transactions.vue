<script setup lang="ts">
import type { Transaction } from "@/types/transaction";
import { getTransactions } from "@/api/TransactionsApi"
import { ref, type Ref } from "vue";
import TransactionsItem from "@/components/TransactionItem.vue";

var transactions: Ref<Transaction[]> = ref([])

getTransactions().then((data: any[]) => {
    transactions.value = data
})

</script>

<template>
    <h1>Transactions</h1>
    <div>There are {{ transactions.length }} transactions</div>
    <table>
        <tbody>
            <tr v-for="transaction in transactions">
                <TransactionsItem :transaction="transaction" />
            </tr>
        </tbody>
    </table>
</template>

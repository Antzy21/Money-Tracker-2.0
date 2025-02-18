<script setup lang="ts">
import type { Transaction } from "@/types/transaction";
import { getTransactions, postCsv } from "@/api/TransactionsApi"
import { ref, type Ref } from "vue";
import TransactionsItem from "@/components/TransactionItem.vue";

var transactions: Ref<Transaction[]> = ref([])

function loadTransactions() {
    getTransactions().then((data: any[]) => {
        transactions.value = data
    })
}

function onFileChanged($event: Event) {
    const target = $event.target as HTMLInputElement;
    if (target && target.files) {
        postCsv(target.files[0]).then(() =>
            loadTransactions()
        )
        target.value = ""
    }
}

loadTransactions()

</script>

<template>
    <h1>Transactions</h1>
    <div class="py-4">
        <h3>Upload Transactions</h3>
        <input type="file" @change="onFileChanged($event)" accept=".csv*" capture class="form-control" />
    </div>
    <table class="table table-dark">
        <thead>
            <tr>
                <th>Contact</th>
                <th>Reference</th>
                <th>Amount</th>
                <th>Categories</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="transaction in transactions">
                <TransactionsItem :transaction="transaction" />
            </tr>
        </tbody>
    </table>
</template>

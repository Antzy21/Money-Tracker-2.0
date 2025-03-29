<script setup lang="ts">
import { postCsv } from "@/api/TransactionsApi";
import CategoryFilter from "@/components/CategoryFilter.vue";
import FadeAnimation from "@/components/FadeAnimation.vue";
import TransactionsItem from "@/components/TransactionItem.vue";
import { useStore } from "@/stores/store";
import type { TransactionUploadResponse } from "@/types/responses/transaction-upload-response";
import { storeToRefs } from "pinia";
import { ref, type Ref } from "vue";

const store = useStore();
const { transactionsFilteredBySelectedCategoryIds } = storeToRefs(store)

const transactionUploadResponse: Ref<TransactionUploadResponse | null> = ref(null);

function onFileChanged($event: Event) {
    const target = $event.target as HTMLInputElement;
    if (target && target.files) {
        postCsv(target.files[0]).then((response: TransactionUploadResponse) => {
            transactionUploadResponse.value = response;
            store.loadTransactions();
        });
        target.value = "";
    }
}

</script>

<template>
    <h1>Transactions</h1>
    <div class="py-4">
        <h3>Upload Transactions</h3>
        <input type="file" @change="onFileChanged($event)" accept=".csv*" capture class="form-control" />
        <FadeAnimation :condition="transactionUploadResponse != null" @clear="transactionUploadResponse = null">
            <div v-if="transactionUploadResponse" class="mt-2 alert alert-success" role="alert">
                File uploaded successfully. {{ transactionUploadResponse.transactions.length }} transactions added. 
                {{ transactionUploadResponse.duplicatesCount }} duplicates ignored.
            </div>
        </FadeAnimation>
    </div>
    <div class="text-center mb-3">
        <CategoryFilter/>
    </div>
    <table class="table table-dark">
        <thead>
            <tr>
                <th>Contact</th>
                <th>Reference</th>
                <th>Amount</th>
                <th>Date</th>
                <th>Categories</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="transaction in transactionsFilteredBySelectedCategoryIds" :key="transaction.id">
                <TransactionsItem :transaction="transaction" />
            </tr>
        </tbody>
    </table>
</template>
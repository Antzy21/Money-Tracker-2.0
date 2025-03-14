<script setup lang="ts">
import type { Transaction } from "@/types/transaction";
import { getTransactions, postCsv } from "@/api/TransactionsApi";
import { ref, type Ref, computed, type ComputedRef } from "vue";
import TransactionsItem from "@/components/TransactionItem.vue";
import CategoryFilter from "@/components/CategoryFilter.vue";
import { Uncategorized, type Category } from "@/types/category";
import type { TransactionUploadResponse } from "@/types/responses/transaction-upload-response";

var transactions: Ref<Transaction[]> = ref([]);
const selectedCategoryIds: Ref<number[]> = ref([]);

const filteredTransactions = computed(() => {
    return transactions.value.filter(transaction => {
        if (transaction.categories.length === 0) {
            return selectedCategoryIds.value.includes(Uncategorized.id);
        }
        return transaction.categories.some(category => selectedCategoryIds.value.includes(category.id));
    });
});

const categories: ComputedRef<Category[]> = computed(() => {
    return transactions.value.reduce((categories: Category[], transaction) => {
        if (transaction.categories.length === 0 && !categories.some(c => c.id === Uncategorized.id)) {
            categories.push(Uncategorized)
        }
        transaction.categories.forEach(category => {
            if (!categories.some(c => c.id === category.id)) {
                categories.push(category)
            }
        })
        return categories;
    }, []);
});

function loadTransactions() {
    getTransactions().then((data: any[]) => {
        transactions.value = data;
        selectedCategoryIds.value = categories.value.map(c => c.id);
    });
}

function onFileChanged($event: Event) {
    const target = $event.target as HTMLInputElement;
    if (target && target.files) {
        postCsv(target.files[0]).then((response: TransactionUploadResponse) =>
            loadTransactions()
        );
        target.value = "";
    }
}

loadTransactions();

</script>

<template>
    <h1>Transactions</h1>
    <div class="py-4">
        <h3>Upload Transactions</h3>
        <input type="file" @change="onFileChanged($event)" accept=".csv*" capture class="form-control" />
    </div>
    <div class="text-center mb-3">
        <CategoryFilter :categories="categories" @update:selectedCategories="selectedCategoryIds = $event" />
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
            <tr v-for="transaction in filteredTransactions" :key="transaction.id">
                <TransactionsItem :transaction="transaction" />
            </tr>
        </tbody>
    </table>
</template>

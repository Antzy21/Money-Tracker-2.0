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
const transactionUploadResponse: Ref<TransactionUploadResponse | null> = ref(null);

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
        postCsv(target.files[0]).then((response: TransactionUploadResponse) => {
            transactionUploadResponse.value = response;
            loadTransactions();
            setTimeout(() => {
                const alertElement = document.querySelector('.alert');
                if (alertElement) {
                    alertElement.classList.add('fade-out');
                    setTimeout(() => {
                        transactionUploadResponse.value = null;
                    }, 990); // Match the duration of the fade-out animation
                }
            }, 5000); // Hide alert after 15 seconds
        });
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
        <div v-if="transactionUploadResponse" class="mt-2 alert alert-success fade-in" role="alert">
            File uploaded successfully. {{ transactionUploadResponse.transactions.length }} transactions added. {{ transactionUploadResponse.duplicatesCount }} duplicates ignored.
        </div>
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

<style scoped>
.fade-in {
    animation: fadeIn 1s;
}

.fade-out {
    animation: fadeOut 1s;
}

@keyframes fadeIn {
    from { opacity: 0; }
    to { opacity: 1; }
}

@keyframes fadeOut {
    from { opacity: 1; }
    to { opacity: 0; }
}
</style>
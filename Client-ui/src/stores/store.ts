import { getCategories } from '@/api/CategoriesApi';
import { getTransactions } from '@/api/TransactionsApi';
import { Uncategorized, type Category } from '@/types/category';
import type { Transaction } from '@/types/transaction';
import { defineStore } from 'pinia'
import { computed, ref, type Ref } from 'vue'

export const useStore = defineStore('store', () => {

    const transactions: Ref<Transaction[]> = ref([]);
    const categories: Ref<Category[]> = ref([]);
    const selectedCategoryIds: Ref<Set<number>> = ref(new Set());

    const transactionsFilteredBySelectedCategoryIds = computed(() => {
        return transactions.value.filter(transaction => {
            if (transaction.categories.length === 0) {
                return selectedCategoryIds.value.has(Uncategorized.id);
            }
            return transaction.categories.some(category => selectedCategoryIds.value.has(category.id));
        });
    });

    function loadTransactions() {
        getTransactions().then((data: any[]) => {
            transactions.value = data;
        });
    }

    function loadCategories() {
        getCategories().then((data: any[]) => {
            categories.value = data
            selectedCategoryIds.value = new Set(data.map(category => category.id).concat(Uncategorized.id));
            loadTransactions();
        })
    }

    loadCategories();

    return {
        transactions,
        categories,
        selectedCategoryIds,
        transactionsFilteredBySelectedCategoryIds,
        loadTransactions,
        loadCategories,
    }
})
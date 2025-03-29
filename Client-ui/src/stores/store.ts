import { getTransactions } from '@/api/TransactionsApi';
import { Uncategorized, type Category } from '@/types/category';
import type { Transaction } from '@/types/transaction';
import { defineStore } from 'pinia'
import { computed, ref, type ComputedRef, type Ref } from 'vue'

export const useStore = defineStore('store', () => {

    const transactions: Ref<Transaction[]> = ref([]);
    const selectedCategoryIds: Ref<number[]> = ref([]);

    const transactionsFilteredBySelectedCategoryIds = computed(() => {
        return transactions.value.filter(transaction => {
            if (transaction.categories.length === 0) {
                return selectedCategoryIds.value.includes(Uncategorized.id);
            }
            return transaction.categories.some(category => selectedCategoryIds.value.includes(category.id));
        });
    });

    const categoriesFromTransactions: ComputedRef<Category[]> = computed(() => {
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
        });
    }

    loadTransactions();

    return {
        transactions,
        selectedCategoryIds,
        categoriesFromTransactions,
        transactionsFilteredBySelectedCategoryIds,
        loadTransactions
    }
})
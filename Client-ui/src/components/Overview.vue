<script setup lang="ts">
import { getTransactions } from '@/api/TransactionsApi';
import { getCategories } from '@/api/CategoriesApi';
import type { Transaction } from '@/types/transaction';
import { Uncategorized, type Category } from '@/types/category';
import { type Ref, ref, computed, type ComputedRef } from 'vue';
import AmountBar from './AmountBar.vue';
import CategoryFilter from './CategoryFilter.vue';

var transactions: Ref<Transaction[]> = ref([])
const selectedTimespan: Ref<string> = ref('month');
const selectedCategoryIds: Ref<number[]> = ref([]);

const categories: ComputedRef<Category[]> = computed(() => {
    return transactions.value.reduce((categories, transaction) => {
        transaction.categories.forEach(category => {
            if (!categories.some(c => c.id === category.id)) {
                categories.push(category)
            }
        })
        return categories;
    }, [Uncategorized]);
});

const filteredTransactions: ComputedRef<Transaction[]> = computed(() => {
    return transactions.value.filter(transaction => {
        if (transaction.categories.length === 0) {
            return selectedCategoryIds.value.includes(Uncategorized.id)
        }
        return transaction.categories.some(category => selectedCategoryIds.value.includes(category.id))
    });
});

const groupedTransactions: ComputedRef<{ [key: string]: Transaction[] }> = computed(() => {
    const groups: { [key: string]: Transaction[] } = {};
    filteredTransactions.value.forEach(transaction => {
        let groupKey = 'month';
        const date = new Date(transaction.date);
        if (selectedTimespan.value === 'week') {
            const startOfWeek = new Date(date.setDate(date.getDate() - date.getDay()));
            groupKey = startOfWeek.toLocaleString('default', { month: 'long', day: 'numeric', year: 'numeric' });
        } else if (selectedTimespan.value === 'month') {
            groupKey = date.toLocaleString('default', { month: 'long', year: 'numeric' });
        } else if (selectedTimespan.value === 'year') {
            groupKey = date.getFullYear().toString();
        }
        if (!groups[groupKey]) {
            groups[groupKey] = [];
        }
        groups[groupKey].push(transaction);
    });
    return groups;
});

const maxAbsAmount: ComputedRef<number> = computed(() => {
    var maxPositiveValue = Object.values(groupedTransactions.value)
        .map(ts => 
            ts.filter(t => t.amount > 0)
            .reduce((acc, t) => acc + t.amount, 0)
        )
        .reduce((curMax, sum) => Math.max(curMax, sum), 0);
    var maxNegativeValue = Object.values(groupedTransactions.value)
        .map(ts => 
            ts.filter(t => t.amount < 0)
            .reduce((acc, t) => acc + t.amount, 0)
        )
        .reduce((curMax, sum) => Math.min(curMax, sum), 0);
    return Math.max(maxPositiveValue, Math.abs(maxNegativeValue));
});

function loadData() {
    getTransactions().then((data: any[]) => {
        transactions.value = data
        selectedCategoryIds.value = categories.value.map(c => c.id)
    })
}

loadData()

</script>

<template>
    <div class="text-center">
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="timespan" id="week" value="week"
                v-model="selectedTimespan">
            <label class="form-check-label" for="week">
                Week
            </label>
        </div>
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="timespan" id="month" value="month"
                v-model="selectedTimespan" checked>
            <label class="form-check-label" for="month">
                Month
            </label>
        </div>
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="timespan" id="year" value="year"
                v-model="selectedTimespan">
            <label class="form-check-label" for="year">
                Year
            </label>
        </div>
        <CategoryFilter class="ms-5 d-inline" :categories="categories"
            @update:selectedCategories="selectedCategoryIds = $event" />
    </div>
    <hr />
    <table class="table table-dark">
        <tbody v-for="(transactions, month) in groupedTransactions" :key="month">
            <tr>
                <td width="10%">
                    {{ month }}
                </td>
                <td>
                    <AmountBar :transactions="transactions" :scale="maxAbsAmount"/>
                </td>
            </tr>
        </tbody>
    </table>
</template>

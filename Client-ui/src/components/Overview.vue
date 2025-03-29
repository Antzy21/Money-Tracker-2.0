<script setup lang="ts">
import type { Transaction } from '@/types/transaction';
import { type Ref, ref, computed, type ComputedRef } from 'vue';
import AmountBar from './AmountBar.vue';
import CategoryFilter from './CategoryFilter.vue';
import { useStore } from '@/stores/store';
import { storeToRefs } from 'pinia';

const store = useStore();
const { transactionsFilteredBySelectedCategoryIds } = storeToRefs(store);

const selectedTimespan: Ref<string> = ref('month');

const groupedTransactions: ComputedRef<{ [key: string]: Transaction[] }> = computed(() => {
    const groups: { [key: string]: Transaction[] } = {};
    transactionsFilteredBySelectedCategoryIds.value.forEach(transaction => {
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
        <CategoryFilter class="ms-5 d-inline"/>
    </div>
    <hr />
    <table class="table table-dark">
        <tbody v-for="(transactions, timespan) in groupedTransactions" :key="timespan">
            <tr>
                <td width="10%">
                    {{ timespan }}
                </td>
                <td>
                    <AmountBar :transactions="transactions" :scale="maxAbsAmount"/>
                </td>
            </tr>
        </tbody>
    </table>
</template>

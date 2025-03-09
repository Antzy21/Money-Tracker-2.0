<script setup lang="ts">
import { getTransactions } from '@/api/TransactionsApi';
import type { Transaction } from '@/types/transaction';
import { type Ref, ref, computed } from 'vue';
import AmountBar from './AmountBar.vue';

var transactions: Ref<Transaction[]> = ref([])
const selectedTimespan: Ref<string> = ref('month');

const groupedTransactions = computed(() => {
    const groups: { [key: string]: Transaction[] } = {};
    transactions.value.forEach(transaction => {
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

function loadTransactions() {
    getTransactions().then((data: any[]) => {
        transactions.value = data
    })
}

loadTransactions()

</script>

<template>
    <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="timespan" id="week" value="week" v-model="selectedTimespan">
        <label class="form-check-label" for="week">
            Week
        </label>
    </div>
    <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="timespan" id="month" value="month" v-model="selectedTimespan" checked>
        <label class="form-check-label" for="month">
            Month
        </label>
    </div>
    <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="timespan" id="year" value="year" v-model="selectedTimespan">
        <label class="form-check-label" for="year">
            Year
        </label>
    </div>
    <table class="table table-dark">
        <tbody v-for="(transactions, month) in groupedTransactions" :key="month">
            <tr>
                <td class="d-inline-block">
                    {{ month }}
                </td>
                <td>
                    <AmountBar :transactions="transactions" />
                </td>
            </tr>
        </tbody>
    </table>
</template>

<script setup lang="ts">
import { getTransactions } from '@/api/TransactionsApi';
import type { Category } from '@/types/category';
import type { CategoryAmount } from '@/types/categoryAmount';
import type { Transaction } from '@/types/transaction';
import { type Ref, ref, computed } from 'vue';
import AmountBar from './AmountBar.vue';

var transactions: Ref<Transaction[]> = ref([])

const groupedTransactions = computed(() => {
    const groups: { [key: string]: Transaction[] } = {};
    transactions.value.forEach(transaction => {
        const month = new Date(transaction.date).toLocaleString('default', { month: 'long', year: 'numeric' });
        if (!groups[month]) {
            groups[month] = [];
        }
        groups[month].push(transaction);
    });
    return groups
});

function loadTransactions() {
    getTransactions().then((data: any[]) => {
        transactions.value = data
    })
}

loadTransactions()

</script>

<template>
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

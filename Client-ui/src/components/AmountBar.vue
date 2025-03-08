<script setup lang="ts">
import type { CategoryAmount } from '@/types/categoryAmount';
import { type ComputedRef, computed } from 'vue';
import { type Transaction } from '@/types/transaction';
import { type Category } from '@/types/category';

const { transactions } = defineProps<{
    transactions: Transaction[],
}>()

const Uncategorized: Category = {
    id: 0,
    name: "Uncategorized",
    colour: "#ffffff",
    regexes: [],
}

const transactionsByCategory: ComputedRef<CategoryAmount[]> = computed(() => {
    const groups: CategoryAmount[] = [];
    transactions.forEach(transaction => {
        let category = transaction.categories[0] ?? Uncategorized;
        let group = groups.find(c => c.category.name == category.name)
        if (!group) {
            group = { amount: 0, category: category }
            groups.push(group);
        }
        group.amount += transaction.amount;
    });
    groups.sort((a, b) => a.amount - b.amount);
    return groups;
});

const totalWidth: ComputedRef<number> = computed(() => {
    return transactionsByCategory.value
        .reduce((acc, t) => acc + normaliseValues(t.amount), 0);
});

const negativePadding: ComputedRef<number> = computed(() => {
    const totalNegative = transactionsByCategory.value
        .filter(t => t.amount < 0)
        .reduce((acc, t) => acc + normaliseValues(t.amount), 0);
    return 500 - totalNegative;
});

function normaliseValues(amount: number): number {
    return Math.min(Math.abs(amount) / 10, 200);
}

</script>

<template>
    <div class="progress-stacked" :style="{'margin-left': negativePadding+'px', 'width': totalWidth+'px'}">
        <div v-for="categoryAmount in transactionsByCategory" class="progress"
            :style="{ 'width': normaliseValues(categoryAmount.amount)+'px'}"
            :title="categoryAmount.category.name">
            <div class="progress-bar" :style="{ 'background-color': categoryAmount.category.colour, 'color': 'black'}"></div>
        </div>
    </div>
</template>

<style scoped>
</style>

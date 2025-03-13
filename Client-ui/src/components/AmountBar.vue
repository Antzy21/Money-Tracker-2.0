<script setup lang="ts">
import type { CategoryAmount } from '@/types/categoryAmount';
import { type ComputedRef, computed } from 'vue';
import { type Transaction } from '@/types/transaction';
import { Uncategorized, type Category } from '@/types/category';

const { transactions, scale } = defineProps<{
    transactions: Transaction[],
    scale: number
}>()

const transactionsByCategory: ComputedRef<CategoryAmount[]> = computed(() => {
    const groups: CategoryAmount[] = [];
    transactions.forEach(transaction => {
        let category: Category = transaction.categories[0] ?? Uncategorized;
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
    const totalWidth = transactionsByCategory.value
        .reduce((acc, t) => acc + Math.abs(t.amount), 0)
    return normaliseValues(totalWidth);
});

const negativePadding: ComputedRef<number> = computed(() => {
    const totalNegative = transactionsByCategory.value
        .filter(t => t.amount < 0)
        .reduce((acc, t) => acc + Math.abs(t.amount), 0);
    return 500 - normaliseValues(totalNegative);
});

function normaliseValues(amount: number): number {
    return Math.abs(amount) / scale * 500;
}

</script>

<template>
    <div class="progress-stacked" :style="{ 'margin-left': negativePadding + 'px', 'width': totalWidth+'px'}">
        <div v-for="categoryAmount in transactionsByCategory" class="progress"
            :style="{ 'width': normaliseValues(categoryAmount.amount)+'px'}" :title="categoryAmount.category.name">
            <div class="progress-bar" :style="{ 'background-color': categoryAmount.category.colour, 'color': 'black'}"></div>
        </div>
    </div>
</template>

<style scoped>
</style>

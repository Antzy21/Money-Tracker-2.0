<script setup lang="ts">
import type { Transaction } from "@/types/transaction";

const { transaction } = defineProps<{
    transaction: Transaction,
}>()

</script>

<template>
    <td>
        {{ transaction.contact }}
    </td>
    <td>
        {{ transaction.reference }}
    </td>
    <td v-if="transaction.amount > 0" style="color: green;">
        £{{ transaction.amount }}
    </td>
    <td v-if="transaction.amount < 0" style="color: red;">
        -£{{ -transaction.amount }}
    </td>
    <td>
        <span v-for="category in transaction.categories">
            <span class="badge" :style="{ 'background-color': category.colour }">
                {{ category.name }}
            </span>
        </span>
    </td>
</template>

<style scoped>
    .badge {
        margin-right: 5px;
        color: black;
        border: 1px solid black;
    }
</style>

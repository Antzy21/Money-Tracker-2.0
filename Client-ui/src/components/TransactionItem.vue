<script setup lang="ts">
import type { Transaction } from "@/types/transaction";
import { computed } from "vue";

const { transaction } = defineProps<{
    transaction: Transaction,
}>()

</script>

<template>
    <td>
        {{ transaction.contact }}
    </td>
    <td v-if="transaction.amount > 0" style="color: green;">
        £{{ transaction.amount }}
    </td>
    <td v-if="transaction.amount < 0" style="color: red;">
        -£{{ -transaction.amount }}
    </td>
</template>

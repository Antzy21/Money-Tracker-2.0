<script setup lang="ts">
import ColouredBadge from "@/components/ColouredBadge.vue";
import { useStore } from "@/stores/store";
import { Uncategorized } from "@/types/category";
import { storeToRefs } from "pinia";
import { computed } from "vue";

const store = useStore();
const { categories } = storeToRefs(store);

const categoriesIncludingUncategorised = computed(() => [...categories.value, Uncategorized]);

function toggleCategory(categoryId: number) {
    if (store.selectedCategoryIds.has(categoryId)) {
        store.selectedCategoryIds.delete(categoryId);
    } else {
        store.selectedCategoryIds.add(categoryId);
    }
}

function doubleClickCategory(categoryId: number) {
    if (store.selectedCategoryIds.has(categoryId) && store.selectedCategoryIds.size == 1) {
        store.selectedCategoryIds = new Set(categoriesIncludingUncategorised.value.map(c => c.id));
    }
    else {
        store.selectedCategoryIds.clear();
        store.selectedCategoryIds.add(categoryId);
    }
}

</script>

<template>
    <div>
        <ColouredBadge v-for="category in categoriesIncludingUncategorised" class="m-1" :key="category.id" :colour="category.colour"
            :disabled="!store.selectedCategoryIds.has(category.id)" @click="toggleCategory(category.id)"
            @dblclick="doubleClickCategory(category.id)">
            {{ category.name }}
        </ColouredBadge>
    </div>
</template>

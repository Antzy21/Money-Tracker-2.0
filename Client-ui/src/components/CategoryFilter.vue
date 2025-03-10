<script setup lang="ts">
import type { Category } from "@/types/category";
import { ref, type Ref, computed, type ComputedRef } from "vue";
import { defineEmits } from "vue";
import ColouredBadge from "@/components/ColouredBadge.vue";

const { categories, selectedDefault = true } = defineProps<{
    categories: Category[],
    selectedDefault?: boolean
}>();

const emit = defineEmits<{
    (e: 'update:selectedCategories', selectedCategoryIds: number[]): void;
}>();

const clickedCategories = ref<Set<number>>(new Set([]));

const selectedCategories: ComputedRef<Set<number>> = computed(() => {
    if (selectedDefault) {
        return new Set(categories.filter(c => !clickedCategories.value.has(c.id)).map(c => c.id));
    } else {
        return clickedCategories.value;
    }
});

function toggleCategory(categoryId: number) {
    if (clickedCategories.value.has(categoryId)) {
        clickedCategories.value.delete(categoryId);
    } else {
        clickedCategories.value.add(categoryId);
    }
    emit('update:selectedCategories', Array.from(selectedCategories.value));
}

function doubleClickCategory(categoryId: number) {
    if (selectedDefault) {
        if (!clickedCategories.value.has(categoryId) && clickedCategories.value.size == categories.length - 1) {
            clickedCategories.value.clear();
        }
        else {
            clickedCategories.value = new Set(categories.filter(c => c.id !== categoryId).map(c => c.id));
            clickedCategories.value.delete(categoryId);
        }
    }
    else {
        if (clickedCategories.value.has(categoryId) && clickedCategories.value.size == 1) {
            clickedCategories.value = new Set(categories.map(c => c.id));
        }
        else {
            clickedCategories.value.clear();
            clickedCategories.value.add(categoryId);
        }
    }
    emit('update:selectedCategories', Array.from(selectedCategories.value));
}

</script>

<template>
    <div>
        <ColouredBadge v-for="category in categories" :key="category.id" :colour="category.colour"
            :disabled="!selectedCategories.has(category.id)" @click="toggleCategory(category.id)"
            @dblclick="doubleClickCategory(category.id)">
            {{ category.name }}
        </ColouredBadge>
    </div>
</template>

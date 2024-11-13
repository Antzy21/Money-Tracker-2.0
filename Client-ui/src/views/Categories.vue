<script setup lang="ts">
import type { Category } from "@/types/category";
import { getCategories } from "@/api/CategoriesApi"
import { ref, type Ref } from "vue";

var categories: Ref<Category[]> = ref([])

getCategories().then((data: any[]) => {
  categories.value = data
})
</script>

<template>
    <h1>Categories</h1>
    <div>There are {{ categories.length }} categories</div>
    <div v-for="category in categories">
        {{ category.name }} {{ category.id }}
    </div>
</template>

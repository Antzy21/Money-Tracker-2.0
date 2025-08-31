<script setup lang="ts">
import type { Category } from "@/types/category";
import { deleteCategory, postCategory } from "@/api/CategoriesApi"
import CategoryItem from "@/components/CategoryItem.vue";
import { useStore } from "@/stores/store";
import { storeToRefs } from "pinia";

const store = useStore()
const { categories } = storeToRefs(store)

var newCategory: Category = generateNewCategory()

function handleEnter(event: Event) {
    if (newCategory.name === "") {
        return
    }

    // Remove focus from textbox input
    const inputElement = (event.target as HTMLInputElement)
    inputElement.blur();

    postCategory(newCategory)
        .then((newCategoryResponse) => {
            newCategory = generateNewCategory()
            categories.value.push(newCategoryResponse)
        });
}

function handleUpdate(category: Category) {
    postCategory(category)
        .then((newCategory) => {
            const index = categories.value.findIndex(c => c.id === category.id)
            categories.value.splice(index, 1, newCategory)
        });
}

function handleDelete(categoryId: number) {
    deleteCategory(categoryId)
        .then(() => {
            const index = categories.value.findIndex(c => c.id === categoryId)
            categories.value.splice(index, 1)
        });
}

function generateNewCategory(): Category {
    return {
        id: 0,
        name: "",
        colour: "#ffffff",
        regexes: [],
    }
}

</script>

<template>
    <h1>Categories</h1>
    <table class="table">
        <tbody>
            <CategoryItem :category="category" v-for="category in categories" @update="handleUpdate"
                @delete="handleDelete" />
            <tr>
                <td></td>
                <td>
                    <input placeholder="...new category" v-model="newCategory.name"
                        v-on:keyup.enter="handleEnter($event)" class="form-control">
                </td>
            </tr>
        </tbody>
    </table>
</template>

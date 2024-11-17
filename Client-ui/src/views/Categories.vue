<script setup lang="ts">
import type { Category } from "@/types/category";
import { deleteCategory, getCategories, postCategory } from "@/api/CategoriesApi"
import { ref, type Ref } from "vue";

var categories: Ref<Category[]> = ref([])
var newCategoryName: string

getCategories().then((data: any[]) => {
    categories.value = data
})

function handleEnter(event: Event) {
    // Remove focus from textbox input
    const inputElement = (event.target as HTMLInputElement)
    inputElement.blur();

    postCategory({ name: newCategoryName })
        .then((newCategory) => {
            newCategoryName = ""
            categories.value.push(newCategory)
        });
}

function handleDelete(category: Category) {
    deleteCategory(category.id)
        .then(() => {
            const index = categories.value.findIndex(c => c.id === category.id)
            categories.value.splice(index, 1)
        });
}

</script>

<template>
    <h1>Categories</h1>
    <div>There are {{ categories.length }} categories</div>
    <table>
        <tbody>
            <tr v-for="category in categories">
                <td>{{ category.name }}</td>
                <td>
                    <button v-on:click="handleDelete(category)">
                        Delete
                    </button>
                </td>
            </tr>
            <tr>
                <td>
                    <input placeholder="...new category" v-model="newCategoryName"
                        v-on:keyup.enter="handleEnter($event)">
                </td>
            </tr>
        </tbody>
    </table>
</template>

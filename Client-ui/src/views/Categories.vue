<script setup lang="ts">
import type { Category } from "@/types/category";
import { deleteCategory, getCategories, postCategory } from "@/api/CategoriesApi"
import { ref, type Ref } from "vue";
import CategoryItem from "@/components/CategoryItem.vue";

var categories: Ref<Category[]> = ref([])
var newCategory: Category = generateNewCategory()

getCategories().then((data: any[]) => {
    categories.value = data
})

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

function generateNewCategory() : Category {
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
    <div>There are {{ categories.length }} categories</div>
    <table>
        <tbody>
            <CategoryItem :category="category" v-for="category in categories" @update="handleUpdate" @delete="handleDelete"/>
            <tr>
                <td></td>
                <td>
                    <input placeholder="...new category" v-model="newCategory.name"
                        v-on:keyup.enter="handleEnter($event)">
                </td>
            </tr>
        </tbody>
    </table>
</template>

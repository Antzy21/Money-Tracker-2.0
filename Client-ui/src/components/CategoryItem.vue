<script setup lang="ts">
import { addCategoryRegex, deleteCategoryRegex } from "@/api/CategoriesApi";
import type { Category } from "@/types/category";
import { reactive, ref, type Ref } from "vue";
import CategoryRegexItem from "./CategoryRegexItem.vue";
import Editable from "./Editable.vue";
import ColouredBadge from "./ColouredBadge.vue";

const { category } = defineProps<{
    category: Category,
}>()

const emit = defineEmits<{
    (e: 'update', category: Category): void,
    (e: 'delete', id: number): void,
}>()

const regexesToShow = reactive(() => {
    if (showRegexes.value) {
        return category.regexes
    } else {
        return []
    }
})

var showRegexes: Ref<boolean> = ref(false);
var newCategoryRegex: string = "";

function toggleShowRegexes() {
    showRegexes.value = !showRegexes.value;
}

function updateCategoryName(categoryName: any) {
    category.name = categoryName
    emit('update', category)
}

function updateCategoryColour(categoryColour: any) {
    category.colour = categoryColour
    emit('update', category)
}

function deleteCategory(event: Event) {
    emit('delete', category.id)
}

function handleEnter(event: Event) {
    if (newCategoryRegex === "") {
        return
    }

    // Remove focus from textbox input
    const inputElement = (event.target as HTMLInputElement)
    inputElement.blur();

    addCategoryRegex(newCategoryRegex, category.id)
        .then((newCategoryRegexResponse) => {
            newCategoryRegex = ""
            category.regexes.push(newCategoryRegexResponse.regex)
        });
}

function handleRegexDelete(regex: string) {
    deleteCategoryRegex(regex)
        .then(() => {
            const index = category.regexes.findIndex(r => r === regex)
            category.regexes.splice(index, 1)
        });
}

</script>

<template>
    <tr>
        <td width="55px">
            <button v-on:click="toggleShowRegexes()" class="btn btn-primary">
                {{ showRegexes ? "Hide" : "Show" }}
            </button>
        </td>
        <td>
            <Editable :model="category.name" @update="updateCategoryName">
                {{ category.name }}
            </Editable>
        </td>
        <td>
            <Editable :model="category.colour" @update="updateCategoryColour" input-type="color">
                <ColouredBadge :colour="category.colour">
                    {{ category.colour }}
                </ColouredBadge>
            </Editable>
        </td>
        <td>
            {{ category.regexes.length }}
        </td>
        <td>
            <button v-on:click="deleteCategory($event)" class="btn btn-danger">
                Delete
            </button>
        </td>
    </tr>
    <CategoryRegexItem :regex="regex" v-for="regex in regexesToShow()" @delete="handleRegexDelete"></CategoryRegexItem>
    <tr v-if="showRegexes">
        <td></td>
        <td>
            <input placeholder="...new regex" v-model="newCategoryRegex" v-on:keyup.enter="handleEnter($event)"
                class="form-control">
        </td>
    </tr>
</template>

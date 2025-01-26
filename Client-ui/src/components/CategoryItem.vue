<script setup lang="ts">
import type { Category } from "@/types/category";
import { nextTick, reactive, ref, useTemplateRef, type Ref } from "vue";

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

const nameInput = useTemplateRef('name-input')

var editMode: Ref<boolean> = ref(false);
var showRegexes: Ref<boolean> = ref(false);

function toggleEditMode() {
    editMode.value = !editMode.value;

    // wait for the input to be rendered before assigning focus
    nextTick(() => {
        if (editMode.value) {
            nameInput.value!.focus()
        }
    })
}

function toggleShowRegexes() {
    showRegexes.value = !showRegexes.value;
}

function updateCategory(event: Event) {
    // Remove focus from textbox input
    const inputElement = (event.target as HTMLInputElement)
    inputElement.blur();

    editMode.value = false;
    emit('update', category)
}

function deleteCategory(event: Event) {
    emit('delete', category.id)
}

</script>

<template>
    <tr>
        <td width="55px">
            <button v-on:click="toggleShowRegexes()">
                {{ showRegexes ? "Hide" : "Show" }}
            </button>
        </td>
        <td v-if="editMode">
            <input ref="name-input" v-model="category.name" v-on:keyup.enter="updateCategory($event)"
                v-on:blur="toggleEditMode()">
        </td>
        <td v-else v-on:click="toggleEditMode()">
            {{ category.name }}
        </td>
        <td>
            {{ category.colour }}
        </td>
        <td>
            {{ category.regexes.length }}
        </td>
        <td>
            <button v-on:click="deleteCategory($event)">
                Delete
            </button>
        </td>
    </tr>
    <tr v-for="regex in regexesToShow()">
        <td></td>
        <td>
            {{ regex }}
        </td>
    </tr>
</template>

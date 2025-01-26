<script setup lang="ts">
import type { Category } from "@/types/category";
import { nextTick, ref, useTemplateRef, type Ref } from "vue";

const { category } = defineProps<{
    category: Category,
}>()

const emit = defineEmits<{
    (e: 'update', category: Category): void,
    (e: 'delete', id: number): void,
}>()

const nameInput = useTemplateRef('name-input')

var editMode: Ref<boolean> = ref(false);

function toggleEditMode() {
    editMode.value = !editMode.value;

    // wait for the input to be rendered before assigning focus
    nextTick(() => {
        if (editMode.value) {
            nameInput.value!.focus()
        }
    })
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
</template>

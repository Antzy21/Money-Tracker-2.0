<script setup lang="ts">
import { nextTick, ref, useTemplateRef, type Ref } from "vue";

const { model, inputType = "text" } = defineProps<{
    model: any,
    inputType?: string,
}>()

const emit = defineEmits<{
    (e: 'update', model: any): void,
}>()

const modelInput = useTemplateRef('model-input')

var editableModel: any = model.value;

var editMode: Ref<boolean> = ref(false);

function onBlur(event: Event) {
    if (inputType == 'color') {
        update(event)
    }
    toggleEditMode()
}

function toggleEditMode() {
    editMode.value = !editMode.value;
    editableModel = model;

    // wait for the input to be rendered before assigning focus
    nextTick(() => {
        if (editMode.value) {
            modelInput.value!.focus()
        }
    })
}

function update(event: Event) {
    if (editableModel !== model) {
        emit('update', editableModel)
    }
    // Remove focus from textbox input
    const inputElement = (event.target as HTMLInputElement)
    inputElement.blur();
}

</script>

<template>
    <div v-if="editMode">
        <input ref="model-input" v-model="editableModel" v-on:keyup.enter="update($event)" :type="inputType"
            v-on:change="update($event)"
            v-on:blur="onBlur($event)">
    </div>
    <div v-else v-on:click="toggleEditMode()">
        <slot>
        </slot>
    </div>
</template>

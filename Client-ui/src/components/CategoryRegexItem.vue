<script setup lang="ts">

const { regex } = defineProps<{
    regex: string,
}>()

const emit = defineEmits<{
    (e: 'delete', regex: string): void,
}>()

function deleteRegex(event: Event) {
    emit('delete', regex)
}

</script>

<template>
    <tr>
        <td></td>
        <td>
            {{ regex }}
        </td>
        <td>
            <button v-on:click="deleteRegex($event)" class="btn btn-danger">
                Delete
            </button>
        </td>
    </tr>
</template>

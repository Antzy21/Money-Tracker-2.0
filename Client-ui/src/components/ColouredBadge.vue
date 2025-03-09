<script setup lang="ts">

const { colour, disabled = false } = defineProps<{
    colour: string,
    disabled?: boolean
}>()

</script>

<template>
    <span class="badge" :style="{ 'background-color': disabled ? colour + '70' : colour, 'border-color': disabled ? 'black' : 'white' }">
        <slot></slot>
    </span>
</template>

<style scoped>
.badge {
    margin-right: 5px;
    color: black;
    border: 1px solid;
}
</style>

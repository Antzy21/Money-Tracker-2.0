<script setup lang="ts">

const { colour, disabled = false } = defineProps<{
    colour: string,
    disabled?: boolean
}>()

</script>

<template>
    <span class="badge" :style="{ 'background-color': disabled ? colour + '70' : colour }">
        <slot></slot>
    </span>
</template>

<style scoped>
</style>

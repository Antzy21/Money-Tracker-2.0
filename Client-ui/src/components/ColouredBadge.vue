<script setup lang="ts">

const { colour } = defineProps<{
    colour: string,
}>()

</script>

<template>
    <span class="badge" :style="{ 'background-color': colour }">
        <slot></slot>
    </span>
</template>

<style scoped>
.badge {
    margin-right: 5px;
    color: black;
    border: 1px solid black;
}
</style>

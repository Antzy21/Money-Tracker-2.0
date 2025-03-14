<script setup lang="ts">
import { ref, watch, type Ref } from 'vue';

const delayed: Ref<boolean> = ref(false);

const { condition, showDuration = 5000 } = defineProps<{
    condition: boolean,
    showDuration?: number
}>()

const emit = defineEmits<{
    (e: 'clear'): void,
}>()


watch(() => condition, (newVal, oldVal) => {
    if (newVal && newVal !== oldVal) {
        delayed.value = true;
        setTimeout(() => {
            document.querySelector('.fade-in')!.classList.add('fade-out');
            setTimeout(() => {
                delayed.value = false;
                emit('clear');
            }, 990);
        }, showDuration);
    }
});

</script>

<template>
    <div v-if="delayed" class="fade-in">
        <slot></slot>
    </div>
</template>

<style scoped>
.fade-in {
    animation: fadeIn 1s;
}

.fade-out {
    animation: fadeOut 1s;
}

@keyframes fadeIn {
    from { opacity: 0; }
    to { opacity: 1; }
}

@keyframes fadeOut {
    from { opacity: 1; }
    to { opacity: 0; }
}
</style>

<script setup lang="ts">
import { healthcheck } from '@/api/HealthCheckApi';
import { ref } from 'vue';

var healthStatus = ref("warning")
var healthcheckInterval = 30 * 1000

runHealthCheck()

setInterval(() => runHealthCheck(), healthcheckInterval)

function runHealthCheck() {
    healthcheck().then((response) => {
        healthStatus.value = response.status == 200 ? "ok" : "error"
    }).catch(() => {
        healthStatus.value = "error"
    })
}

</script>

<template>
    <span class="health" :class="healthStatus">Server Status</span>
</template>

<style scoped>
.health {
    padding-left: 1em;
    padding-right: 1em;
    padding-top: 0.25em;
    padding-bottom: 0.25em;
    border-radius: 1em;
    color: black;
}

.warning {
    background-color: yellow;
    border: 2px solid orange;
}

.ok {
    background-color: lightgreen;
    border: 2px solid green;
}

.error {
    background-color: red;
    font-weight: bold;
    border: 2px solid maroon;
}
</style>

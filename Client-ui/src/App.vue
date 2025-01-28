<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import HealthCheck from './components/HealthCheck.vue'
</script>

<template>
  <header>
    <nav>
      <RouterLink to="/">Home</RouterLink>
      <RouterLink to="/categories">Categories</RouterLink>
      <RouterLink to="/transactions">Transactions</RouterLink>
      <HealthCheck></HealthCheck>
    </nav>
  </header>

  <RouterView />
</template>

<style scoped>
header {
  max-height: 100vh;
}

nav {
  width: 100%;
  text-align: center;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 1rem;
  color: var(--color-text);
}

nav a:first-of-type {
  border: 0;
}
</style>

<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import HealthCheck from './components/HealthCheck.vue'
</script>

<template>
  <header>
    <nav class="navbar navbar-expand-sm">

      <div class="container-fluid">
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
          <div class="navbar-nav">
            <RouterLink class="nav-link" to="/">Home</RouterLink>
            <RouterLink class="nav-link" to="/categories">Categories</RouterLink>
            <RouterLink class="nav-link" to="/transactions">Transactions</RouterLink>
            <RouterLink class="nav-link" to="/admin">Admin</RouterLink>
          </div>
        </div>
        <HealthCheck></HealthCheck>
      </div>
    </nav>
  </header>

  <RouterView />
</template>

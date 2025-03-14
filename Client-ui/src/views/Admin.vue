<script setup lang="ts">
import { deleteAllTransactions, deleteAllCategories } from '@/api/AdminApi'

function deleteTransactions() {
  deleteAllTransactions().then(response => {
    if (response.ok) {
      alert('All transactions deleted successfully');
    } else {
      alert('Failed to delete transactions');
    }
  });
}

function deleteCategories() {
  deleteAllCategories().then(response => {
    if (response.ok) {
      alert('All categories deleted successfully');
    } else {
      alert('Failed to delete categories');
    }
  });
}
</script>

<template>
  <h1>Admin</h1>
  <div class="p-3">
    <button class="btn btn-danger" @click="deleteTransactions">Delete all transactions</button>
  </div>
  <div class="p-3">
    <button class="btn btn-danger" @click="deleteCategories">Delete all categories</button>
  </div>
</template>

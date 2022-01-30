<template>
  <div class="container">
    <h1>Transactions</h1>
    <TransactionTable
      :transactions="transactions"
      :showCount="true"
    />
    <Uncatagorised :transactions="uncategorisedTransactions"/>
    <Categories />
    <UploadCsv />
  </div>
</template>

<script>
import { mapState } from 'vuex'
import Uncatagorised from './Uncatagorised.vue'
import TransactionTable from './TransactionTable.vue'
import Categories from './Categories.vue'
import UploadCsv from './UploadCsv.vue'

export default {
  name: 'Transactions',
  components: {
    Uncatagorised,
    Categories,
    UploadCsv,
    TransactionTable
  },
  computed: {
    ...mapState(['transactions', 'categories']),
    uncategorisedTransactions() {
      if (!this.transactions) {
        return [];
      }
      return this.transactions.filter(t => t.category == null)
    },
  },
}
</script>
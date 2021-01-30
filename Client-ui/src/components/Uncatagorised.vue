<template>
  <div>
    <h1>Uncatagorised</h1>
    <TransactionTable :transactions="transactions"/>
  </div>
</template>

<script>
import TransactionTable from './TransactionTable.vue'

export default {
  name: 'Uncatagorised',
  props: {
    transactions: Array,
  },
  components: {
    TransactionTable,
  },
  data() {
    return {

    }
  },
  methods: {
  }
}
</script>
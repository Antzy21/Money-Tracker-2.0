<template>
  <table>
    <tr>
        <th>Id</th>
        <th>Contact</th>
        <th>Reference</th>
        <th>Amount</th>
    </tr>
    <TransactionRow v-for="t in transactions" :key="t.id" :transaction="t"/>
  </table>
</template>

<script>
import TransactionRow from './TransactionRow.vue'

export default {
  name: 'TransactionTable',
  components: {
    TransactionRow,
  },
  props: {
    transactions: Array,
  },
  methods: {
  }
}
</script>
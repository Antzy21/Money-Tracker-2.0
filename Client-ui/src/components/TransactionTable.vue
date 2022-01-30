<template>
  <table>
    <div v-if="showCount">
      Number of Transactions: {{numberOfTransactions}}
    </div>
    <tr>
        <th>Id</th>
        <th>Contact</th>
        <th>Reference</th>
        <th>Amount</th>
        <th>Category</th>
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
    transactions: {
      type: Array,
      default: () => [],
    },
    showCount: {
      type: Boolean,
      default: false
    },
  },
  computed: {
    numberOfTransactions() {
      return this.transactions.length;
    }
  },
  methods: {
  }
}
</script>
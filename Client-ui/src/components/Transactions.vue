<template>
  <div class="container">
    <h1>Transactions</h1>
    <TransactionTable
      :transactions="transactions"
      :showCount="true"
    />
    <Uncatagorised :transactions="uncategorisedTransactions"/>
    <Contacts />
    <References />
    <UploadCsv />
  </div>
</template>

<script>
import { mapState } from 'vuex'
import Uncatagorised from './Uncatagorised.vue'
import TransactionTable from './TransactionTable.vue'
import Contacts from './Contacts.vue'
import References from './References.vue'
import UploadCsv from './UploadCsv.vue'

export default {
  name: 'Transactions',
  components: {
    Uncatagorised,
    Contacts,
    References,
    UploadCsv,
    TransactionTable
  },
  computed: {
    ...mapState(['transactions', 'contacts', 'references']),
    uncategorisedTransactions() {
      if (!this.transactions) {
        return [];
      }
      return this.transactions.filter(t => t.contact == null || t.reference == null)
    },
  },
}
</script>
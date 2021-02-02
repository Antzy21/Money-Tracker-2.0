<template>
  <div>
    <h1>Transactions</h1>
    <Uncatagorised :transactions="uncategorisedTransactions"/>
    <Contacts />
    <References />
    <!-- <UploadCsv /> -->
  </div>
</template>

<script>
import { mapState } from 'vuex'
import Uncatagorised from './Uncatagorised.vue'
import Contacts from './Contacts.vue'
import References from './References.vue'
//import UploadCsv from './UploadCsv.vue'

export default {
  name: 'Transactions',
  components: {
    Uncatagorised,
    Contacts,
    References,
    //UploadCsv,
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
<template>
  <div>
    <TransactionTable v-for="group in timeSplitTransactions"
      :key="group.key"
      :transactions="group.data"
    />
  </div>
</template>

<script>
import { mapState } from 'vuex'
import TransactionTable from '../TransactionTable.vue'

const timeSpans = {
  Years: 0,
  Weeks: 1,
  Days: 2
}

export default {
  name: 'BarChart',
  components: {
    TransactionTable,
  },
  data() {
    return {
      timePeriodSplit: timeSpans.Years,
    }
  },
  computed: {
    ...mapState(['transactions', 'contacts', 'references']),
    timeSplitTransactions() {
      let copiedTransactions = [ ...this.transactions ];
      copiedTransactions = copiedTransactions.sort((t1, t2) => Date.parse(t1.date) - Date.parse(t2.date));

      let groupKey = 0;
      const transactionGroups = copiedTransactions.reduce((groupedTransactions, transaction) => {
        var timeSplit = transaction.date.split(('-'))[this.timePeriodSplit];
        if (groupedTransactions[timeSplit] == undefined){
          groupedTransactions[timeSplit] = {key: groupKey++, data: []};
        }
        groupedTransactions[timeSplit].data.push(transaction)
        return groupedTransactions;
      }, {});
      
      return transactionGroups;
    }
  },
}
</script>
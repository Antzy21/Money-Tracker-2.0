<template>
  <div class="container">
    <TransactionChart
    />
  </div>
</template>

<script>
import { mapState } from 'vuex'
import TransactionChart from './TransactionChart.vue'

const timeSpans = {
  Years: 0,
  Weeks: 1,
  Days: 2
}

export default {
  components: {
    TransactionChart,
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
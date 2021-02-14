<template>
  <div>
  </div>
</template>

<script>
import { mapState } from 'vuex'

const timeSpans = {
  Years: 0,
  Weeks: 1,
  Days: 2
}

export default {
  name: 'BarChart',
  components: {
  },
  data() {
    return {
      timePeriodSplit: timeSpans.Days,
    }
  },
  computed: {
    ...mapState(['transactions', 'contacts', 'references']),
    timeSplitTransactions() {
      let copiedTransactions = [ ...this.transactions ];
      copiedTransactions = copiedTransactions.sort((t1, t2) => Date.parse(t1.date) - Date.parse(t2.date));

      let groupKey = 0;
      const transactionGroups = copiedTransactions.reduce((r, transaction) => {
        var timeSplit = transaction.date.split(('-'))[this.timePeriodSplit];
        (r[timeSplit])? r[timeSplit].data.push(transaction) : r[timeSplit] = {group: String(groupKey++), data: [transaction]};
        return r;
      });
      return transactionGroups;
    }
  },
}
</script>
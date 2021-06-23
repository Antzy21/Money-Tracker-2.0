<template>
  <div class="container">
    <div v-for="(value, key) in timeSpans" class="form-check" :key="key">
      <input v-model="timePeriodSplit" :value="value" class="form-check-input" type="radio" name="flexRadioDefault" id="timePeriodSplit">
      <label class="form-check-label" for="timePeriodSplit">
        {{key}}
      </label>
    </div>
    <TransactionChart
      :chartData="chartData"
      :chartOptions="chartOptions"
    />
  </div>
</template>

<script>
import { mapState } from 'vuex'
import TransactionChart from './TransactionChart.vue'

export default {
  components: {
    TransactionChart,
  },
  data() {
    return {
      timePeriodSplit: 0,
      timeSpans: {
        "Years": 0,
        "Months": 1,
        "Weeks": 2,
        "Days": 3
      },
    }    
  },
  computed: {
    ...mapState(['transactions', 'contacts', 'references']),
    timeSplitTransactions() {
      let copiedTransactions = [ ...this.transactions ];
      copiedTransactions = copiedTransactions.sort((t1, t2) => Date.parse(t1.date) - Date.parse(t2.date));
      const transactionGroups = copiedTransactions.reduce((groupedTransactions, transaction) => {
        console.log(groupedTransactions);
        var timeSplit = this.getTimeSplit(transaction);
        console.log(timeSplit);
        if (groupedTransactions.find(gt => gt.key == timeSplit) == undefined) {
          groupedTransactions.push({key: timeSplit, data: []});
        }
        groupedTransactions.find(gt => gt.key == timeSplit).data.push(transaction)
        return groupedTransactions;
      }, []);
      
      return transactionGroups;
    },
    chartData() {
      return {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
          label: '# of Votes',
          data: [12, 19, 3, 5, 2, 3],
          backgroundColor: [
              'rgba(255, 99, 132, 0.2)',
              'rgba(54, 162, 235, 0.2)',
              'rgba(255, 206, 86, 0.2)',
              'rgba(75, 192, 192, 0.2)',
              'rgba(153, 102, 255, 0.2)',
              'rgba(255, 159, 64, 0.2)'
          ],
          borderColor: [
              'rgba(255, 99, 132, 1)',
              'rgba(54, 162, 235, 1)',
              'rgba(255, 206, 86, 1)',
              'rgba(75, 192, 192, 1)',
              'rgba(153, 102, 255, 1)',
              'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 5,
          indexAxis: 'y',
        }]
      }
    },
    chartOptions() {
      return {
        // Elements options apply to all of the options unless overridden in a dataset
        // In this case, we are setting the border of each horizontal bar to be 2px wide
        elements: {
          bar: {
            borderWidth: 2,
          }
        },
        responsive: true,
        plugins: {
          legend: {
            position: 'right',
          },
          title: {
            display: true,
            text: 'Chart.js Horizontal Bar Chart'
          }
        }
      }
    }
  },
  methods: {
    getTimeSplit(transaction) {
      switch (this.timePeriodSplit) {
        case this.timeSpans.Years:
          return transaction.date.split(('-'))[0];
        case this.timeSpans.Months:
          return transaction.date.substring(0,7);
        case this.timeSpans.Weeks:
          var x = new Date(transaction.date);
          var firstDateOfWeek = new Date(x.setDate(x.getDate()-x.getDay()))
          var weekDate = `${firstDateOfWeek.getFullYear()}-${firstDateOfWeek.getMonth()}-${firstDateOfWeek.getDate()}`
          return weekDate;
        case this.timeSpans.Days:
          return transaction.date.substring(0,10);
      }
    }
  }
}
</script>
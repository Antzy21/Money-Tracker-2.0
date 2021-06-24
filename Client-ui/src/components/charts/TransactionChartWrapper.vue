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
      timePeriodSplit: 1,
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
        var timeSplit = this.getTimeSplit(transaction);
        if (groupedTransactions.find(gt => gt.key == timeSplit) == undefined) {
          groupedTransactions.push({key: timeSplit, data: []});
        }
        groupedTransactions.find(gt => gt.key == timeSplit).data.push(transaction)
        return groupedTransactions;
      }, []);
      
      return transactionGroups;
    },
    formattedData() {
      return this.timeSplitTransactions.map(t => {
        return t.data.reduce((accumulatedValue, tran) => {
          return accumulatedValue + parseFloat(tran.amount)*100
        },0) / 100
      })
    },
    formattedLabels() {
      return this.timeSplitTransactions.map(t => t.key)
    },
    chartData() {
      return {
        labels: this.formattedLabels,
        datasets: [{
            categoryPercentage: 1,
            barPercentage: 1,
            borderWidth: 10,
            borderRadius: 20,
            //grouped: true,
            //barThickness: 4,
            //maxBarThickness: 38,
            //minBarLength: 2,
            backgroundColor: 'rgb(220,120,165)',
            hoverBackgroundColor: 'rgb(200,100,145)',
            data: this.formattedData,
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
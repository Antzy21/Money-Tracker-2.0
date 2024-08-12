<script setup lang="ts">
import { computed } from 'vue'
import TransactionChart from './TransactionChart.vue'
import type { Transaction } from '@/types/transaction'

import { useTransactionStore } from '@/stores/transactions'
const transactionStore = useTransactionStore()

const timeSpans = ['Years', 'Months', 'Weeks', 'Days'] as const
type TimeSpan = (typeof timeSpans)[number]
let timePeriodSplit: TimeSpan = 'Years'

let chartTypes = ["Combined", "PosNeg"]
type ChartType = (typeof chartTypes)[number]
let chartType: ChartType = "Combined"

let basicDataSetValues: any = {
  categoryPercentage: 0.8,
  barPercentage: 1,
  borderWidth: 1
  //borderRadius: 20,
  //grouped: true,
  //barThickness: 4,
  //maxBarThickness: 38,
  //minBarLength: 2,
}

const timeSplitTransactions = computed(() => {
  let copiedTransactions = [...transactionStore.transactions]
  copiedTransactions = copiedTransactions.sort((t1, t2) => t1.date.getTime() - t2.date.getTime())
  const transactionGroups = copiedTransactions.reduce(
    (groupedTransactions: { [id: number]: Transaction[] }, transaction) => {
      var timeSplit: number = getTimeSplit(transaction)
      if (groupedTransactions[timeSplit] == undefined) {
        groupedTransactions[timeSplit] = []
      }
      groupedTransactions[timeSplit].push(transaction)
      return groupedTransactions
    },
    {}
  )

  return transactionGroups
})
const formattedPositiveData = computed(() => {
  return Object.entries(timeSplitTransactions.value).map(([,t]) => {
    return (
      t.reduce((accumulatedValue: number, transaction: Transaction) => {
        const tranAmount = transaction.amount
        if (tranAmount < 0) {
          return accumulatedValue
        }
        return accumulatedValue + tranAmount * 100
      }, 0) / 100
    )
  })
})
const formattedNegativeData = computed(() => {
  return Object.entries(timeSplitTransactions.value).map(([,t]) => {
    return (
      t.reduce((accumulatedValue: number, transaction: Transaction) => {
        const tranAmount = transaction.amount
        if (tranAmount > 0) {
          return accumulatedValue
        }
        return accumulatedValue + tranAmount * 100
      }, 0) / 100
    )
  })
})
// const formattedData = computed(() => {
//   return Object.entries(timeSplitTransactions.value).map(([,t]) => {
//     return (
//       t.reduce((accumulatedValue: number, transaction: Transaction) => {
//         const tranAmount = transaction.amount
//         return accumulatedValue + tranAmount * 100
//       }, 0) / 100
//     )
//   })
// })
const formattedLabels = computed(() => {
  return Object.entries(timeSplitTransactions.value).map(([key,]) => key)
})
const chartDataSets = computed(() => {
  switch (chartType) {
    case "Combined":
      return [
        {
          ...basicDataSetValues,
          backgroundColor: 'rgb(220,120,165)',
          hoverBackgroundColor: 'rgb(200,100,145)',
          label: 'Net Income/Expenses',
          data: formattedPositiveData
        }
      ]
    case "PosNeg":
    default:
      return [
        {
          ...basicDataSetValues,
          backgroundColor: 'rgb(220,120,165)',
          hoverBackgroundColor: 'rgb(200,100,145)',
          label: 'Income',
          data: formattedPositiveData
        },
        {
          ...basicDataSetValues,
          backgroundColor: 'rgb(20,120,165)',
          hoverBackgroundColor: 'rgb(20,100,145)',
          label: 'Expenses',
          data: formattedNegativeData
        }
      ]
  }
})
const chartData = computed(() => {
  return {
    datasets: chartDataSets.value,
    labels: formattedLabels.value
  }
})
const chartOptions = computed(() => {
  return {
    // Elements options apply to all of the options unless overridden in a dataset
    // In this case, we are setting the border of each horizontal bar to be 2px wide
    elements: {
      bar: {
        borderWidth: 2
      }
    },
    responsive: true,
    scales: {
      x: {
        stacked: true
      },
      y: {
        stacked: true
      }
    },
    plugins: {
      legend: {
        position: 'right'
      },
      title: {
        display: true,
        text: 'Chart.js Horizontal Bar Chart'
      }
    }
  }
})

function getTimeSplit(transaction: Transaction): Number {
  switch (timePeriodSplit) {
    case 'Years':
      return transaction.date.getFullYear()
    case 'Months':
      return transaction.date.getMonth()
    case 'Weeks':
      return transaction.date.getDay()
    // var x = new Date(transaction.date)
    // var firstDateOfWeek = new Date(x.setDate(x.getDate() - x.getDay()))
    // var weekDate = `${firstDateOfWeek.getFullYear()}-${firstDateOfWeek.getMonth()}-${firstDateOfWeek.getDate()}`
    // return weekDate
    case 'Days':
      return transaction.date.getDate()
    default:
      return transaction.date.getDate()
  }
}
</script>

<template>
  <div class="container">
    <div class="row">
      <div class="col-3">
        <h3>Options</h3>
        <hr />
        <h4>Timespan</h4>
        <div v-for="(value, key) in timeSpans" class="form-check" :key="key">
          <input
            v-model="timePeriodSplit"
            :value="value"
            class="form-check-input"
            type="radio"
            name="timePeriodSplitRadio"
            id="timePeriodSplit"
          />
          <label class="form-check-label" for="timePeriodSplit">
            {{ key }}
          </label>
        </div>
        <hr />
        <h4>Display</h4>
        <div v-for="(value, key) in chartTypes" class="form-check" :key="key">
          <input
            v-model="chartType"
            :value="value"
            class="form-check-input"
            type="radio"
            name="chartTypeRadio"
            id="chartType"
          />
          <label class="form-check-label" for="chartType">
            {{ key }}
          </label>
        </div>
        <hr />
      </div>
      <div class="col">
        <TransactionChart :chartData="chartData" :chartOptions="chartOptions" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import debounce from 'debounce'
import Chart, { type ChartData, type ChartOptions } from 'chart.js/auto'
import { watch } from 'vue'

let myChart: any = null;

const props = defineProps<{
  chartData: ChartData
  chartOptions: ChartOptions
}>();

function debouncedRedraw() {
  debounce(() => {
    redrawChart(props.chartData, props.chartOptions)
  }, 800)
}

watch(props, (_) => {
  //console.log(value);
  debouncedRedraw()
})

function initChart(data: ChartData, options: ChartOptions) {
  const ctx = document.getElementById('myChart');
  myChart = new Chart(ctx, {
    type: 'bar',
    data,
    options
  })
}
function destroyChart() {
  if (myChart) {
    myChart.destroy()
  }
}
function redrawChart(chartData: ChartData, chartOptions: ChartOptions) {
  destroyChart()
  initChart(chartData, chartOptions)
}

initChart(props.chartData, props.chartOptions)

</script>

<template>
  <canvas ref="canvas" id="myChart" width="400" height="400" />
</template>
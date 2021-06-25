<template>
  <canvas ref="canvas" id="myChart" width="400" height="400"/>
</template>

<script>
import { defineComponent } from 'vue'
import Chart from 'chart.js/auto'

export default defineComponent({
  data() {
    return {
      myChart: null
    }
  },
  props: {
    chartData: {
      type: Object,
      required: true
    },
    chartOptions: {
      type: Object,
      required: false
    },
  },
  watch: {
    chartData(value) {
      if (value.datasets) {
        this.destroyChart();
        this.initChart(value, this.chartOptions)
      }
    },
    chartOptions(value) {
      this.destroyChart();
      this.initChart(this.chartData, value)
    }
  },
  beforeDestroy() {
    this.destroyChart();
  },
  mounted() {
    this.initChart(this.chartData, this.chartOptions);
  },
  methods: {
    initChart(data, options) {
      console.log('initialising chart')
      console.log(data)
      console.log(options)
      this.myChart = new Chart(
        document.getElementById('myChart'),
        {
          type: 'bar',
          data,
          options,
        }
      );
    },
    destroyChart() {
      if(this.myChart) {
        console.log('destroying chart')
        this.myChart.destroy();
      } 
    }
  },
})
</script>
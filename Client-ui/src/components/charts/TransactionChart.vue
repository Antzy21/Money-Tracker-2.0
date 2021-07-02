<template>
  <canvas ref="canvas" id="myChart" width="400" height="400"/>
</template>

<script>
import { defineComponent } from 'vue'
import debounce from 'lodash/debounce'
import Chart from 'chart.js/auto'

export default defineComponent({
  data() {
    return {
      myChart: null,
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
      //console.log(value);
      //this.myChart.update();
      this.debouncedRedraw(value, this.chartOptions);
    },
    chartOptions(value) {
      //console.log(value);
      this.debouncedRedraw(this.chartData, value);
    },
  },
  beforeDestroy() {
    this.destroyChart();
  },
  mounted() {
    this.initChart(this.chartData, this.chartOptions);
  },
  created() {
    this.debouncedRedraw = debounce((chartData, chartOptions) => {
      this.redrawChart(chartData, chartOptions)
    }, 800);
  },
  methods: {
    initChart(data, options) {
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
        this.myChart.destroy();
      }
    },
    redrawChart(chartData, chartOptions) {
      this.destroyChart();
      this.initChart(chartData, chartOptions);
    },
  },
})
</script>
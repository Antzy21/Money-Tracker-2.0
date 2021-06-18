<template>
  <div class="home">
    <TransactionChartWrapper />
  </div>
</template>

<script>
import TransactionChartWrapper from '../components/charts/TransactionChartWrapper.vue'

export default {
  name: 'Home',
  components: {
    TransactionChartWrapper,
  }
}
</script>

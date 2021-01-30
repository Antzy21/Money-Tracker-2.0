<template>
  <div id="app">
    <Transactions />
  </div>
</template>

<script>
import {mapActions} from 'vuex'
import Transactions from './components/Transactions.vue'

export default {
  name: 'App',
  components: {
    Transactions,
  },
  created() {
    this.initialise();
  },
  methods: {
    ...mapActions(['initialise'])
  },
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>

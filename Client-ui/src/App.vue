<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">Home</router-link>
      <router-link to="/temp">Temp</router-link>
      <router-link to="/contacts">Contacts</router-link>
      <router-link to="/references">References</router-link>
    </div>
    <router-view/>
  </div>
</template>

<script>
import {mapActions} from 'vuex'
import 'bootstrap/scss/bootstrap.scss'

export default {
  name: 'App',
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
  font-family: Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  background: #cee8f2;
  height: 100%;
}

body {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  overflow: auto;
  margin: 0px;
}

#nav {
  background: #1F2E55;
  padding: 20px;
}

#nav a {
  font-weight: bold;
  color: #c1c9dc;
  padding: 0 1em;
  text-decoration: none;
}

#nav a.router-link-exact-active {
  opacity: 0.6;
}
</style>

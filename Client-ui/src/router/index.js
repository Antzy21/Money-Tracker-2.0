import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import TempEverythingPage from '../views/TempEverythingPage.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/temp',
    name: 'Temp',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: TempEverythingPage
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router

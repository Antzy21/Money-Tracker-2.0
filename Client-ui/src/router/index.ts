import { createRouter, createWebHistory } from 'vue-router'
import Categories from '@/views/Categories.vue'
import Transactions from '@/views/Transactions.vue'
import HomeView from '@/views/Home.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomeView
  },
  {
    path: '/categories',
    name: 'Categories',
    component: Categories
  },
  {
    path: '/transactions',
    name: 'Transactions',
    component: Transactions
  },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router